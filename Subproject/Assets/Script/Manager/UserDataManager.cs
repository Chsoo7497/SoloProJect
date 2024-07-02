using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Unity;
using Firebase.Database;
using Firebase.Extensions;
using System.Threading.Tasks;

namespace UserDataManager
{
    public class UserDatabase
    {
        public string Nickname;
        public int Rank;
        public UserDatabase(string NewNickname)
        {
            this.Nickname = NewNickname;
            this.Rank = 0;
        }
        public void GetUser(UserDatabase Data)
        {
            this.Nickname = Data.Nickname;
            this.Rank = Data.Rank;
        }
    }
    public class UserDataManager
    {
        private static UserDataManager Instance = null;

        public static UserDataManager GetInstance()
        {
            if (Instance == null)
            {
                Instance = new UserDataManager();
                Instance.Init();
            }
            return Instance;
        }
        private FirebaseAuth Auth;
        private FirebaseUser User;
        private DatabaseReference DatabaseRef;
        public string UserId => User.UserId;
        public UserDatabase UserData;
        public Action<bool> LogInState;
        public Action<string> AsyncError;
        public void Init()
        {
            Auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
            Auth.StateChanged += OnIDChanged;
            DatabaseRef = Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference;
            DatabaseRef.ChildChanged += OnDataChanged;
        }
        private void OnIDChanged(object sender, EventArgs e)
        {
            if (Auth.CurrentUser != User)
            {
                bool Sgined = Auth.CurrentUser != null;
                if (!Sgined && User != null)
                {
                    Debug.Log("SignOut");
                    LogInState?.Invoke(false);
                }
                User = Auth.CurrentUser;
                if (Sgined)
                {
                    Debug.Log("SignIn");
                    LogInState?.Invoke(true);
                    ReadUser(UserId);
                }
            }
        }
        private void OnDataChanged(object sender, ChildChangedEventArgs e)
        {
            if (e.DatabaseError != null)
            {
                Debug.Log(e.DatabaseError.Message);
            }

        }

        public async void CreateUser(string Id, string Passward, string Nickname)
        {
            UserDatabase Data = new UserDatabase(Nickname);
            string Json = JsonUtility.ToJson(Data);

            await Auth.CreateUserWithEmailAndPasswordAsync(Id, Passward).ContinueWith(
                task =>
                {
                    if (task.IsCanceled)
                    {
                        Debug.Log("CreateUser Canceled error");
                        AsyncError?.Invoke("Canceled error");
                        return;
                    }
                    if (task.IsFaulted)
                    {
                        CheckError(task.Exception as AggregateException);
                        //Debug.Log("CreateUser Faulted error");
                        AsyncError?.Invoke("계정 생성 실패");
                        return;
                    }
                    //User = task.Result
                    Debug.Log("CreateUser");
                    DatabaseRef.Child("UserData").Child(UserId).SetRawJsonValueAsync(Json);
                }, TaskScheduler.FromCurrentSynchronizationContext());

        }
        public async void LogIn(string Id, string Passward)
        {
            await Auth.SignInWithEmailAndPasswordAsync(Id, Passward).ContinueWith(
                task =>
                {
                    if (task.IsCanceled)
                    {
                        Debug.Log("Canceled error");
                        AsyncError?.Invoke("SignIn Canceled error");
                        return;
                    }
                    if (task.IsFaulted)
                    {
                        CheckError(task.Exception as AggregateException);
                        //Debug.Log("SignIn Faulted error");
                        AsyncError?.Invoke("로그인 실패");
                        return;
                    }
                    //User = task.Result;
                    Debug.Log("SignIn");
                }, TaskScheduler.FromCurrentSynchronizationContext());
        }
        public void LogOut()
        {
            Auth.SignOut();
            Debug.Log("SignOut");
        }
        public void ReadUser(string Id)
        {
            DatabaseRef.Child("UserData").Child(Id).GetValueAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("Id error");
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot Snapshot = task.Result;

                    foreach (DataSnapshot data in Snapshot.Children)
                    {
                        IDictionary Data = (IDictionary)data.Value;
                        UserData.Nickname = (string)Data["Nickname"];
                        UserData.Rank = (int)Data["Rank"];
                    }
                }

            });
        }
        void CheckError(AggregateException ex)
        {
            if (ex != null)
            {
                Firebase.FirebaseException fbEx = null;
                foreach (Exception e in ex.InnerExceptions)
                {
                    fbEx = e as Firebase.FirebaseException;
                    if (fbEx != null)
                        break;
                }

                if (fbEx != null)
                {
                    Debug.LogError("Encountered a FirebaseException:" + fbEx.Message);
                }
            }
        }
    }
}