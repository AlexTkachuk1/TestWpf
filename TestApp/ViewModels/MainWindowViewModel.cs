using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Input;
using TestApp.Infrastructure.Commands;
using TestApp.ViewModels.Base;

namespace TestApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Заголовок и статус
        private string _title = "Анализ статистики";

        /// <summary> Заголовок окна </summary>
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        private string _status = "Готов к работе";

        /// <summary>Статус</summary>
        public string Status
        {
            get => _status;
            set => Set(ref _status, value);
        }

        #endregion

        #region Команды

        public ICommand CloseApplicationCommand { get; }
        private bool CanCloseApplicationCommandExecute(object p) => true;

        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        #endregion

        public MainWindowViewModel()
        {
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            GetAnimeFigureCommand = new LambdaCommand(OnGetAllAnimeFiguresCommandExecuted, CanGetAllAnimeFiguresCommandExecute);
            GetAnimeFigureCommand = new LambdaCommand(OnGetAnimeFigureExecuted, CanGetAnimeFigureExecute);
            CreateAnimeFigureCommand = new LambdaCommand(OnCreateAnimeFigureCommandExecuted, CanCreateAnimeFigureCommandExecute);
            UpdateAnimeFigureCommand = new LambdaCommand(OnUpdateAnimeFigureCommandExecuted, CanUpdateAnimeFigureCommandExecute);
            DeleteAnimeFigureCommand = new LambdaCommand(OnDeleteAnimeFigureExecuted, CanDeleteAnimeFigureExecute);
        }

        #region Методы запросов к API 

        public const string rest_url = @"https://localhost:44322/Rest/APIServerAsync/";

        private string figureId = Guid.Empty.ToString();
        public string FigureId
        {
            get => figureId;
            set => Set(ref figureId, value);
        }
        private string figureName = "Стандартное изображение"; 
        public string FigureName
        {
            get => figureName;
            set => Set(ref figureName, value);
        }

        //private string figureUrl ="/Images/320x220.png";
        private string figureUrl = "/Images/320x220.png";
        public string FigureUrl
        {
            get => figureUrl;
            set => Set(ref figureUrl, value);
        }

        #region Комманды для API

        #region GetAllAnimeFigures

        public ICommand GetAllAnimeFiguresCommand { get; }
        private bool CanGetAllAnimeFiguresCommandExecute(object p) => true;

        private void OnGetAllAnimeFiguresCommandExecuted(object p)
        {
            WebRequest reqGET = WebRequest.Create(rest_url);
            reqGET.Method = "GET";
            WebResponse resp = reqGET.GetResponse();
            Stream stream = resp.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string s = sr.ReadToEnd();


        }
        #endregion


        #region GetAnimeFigure
        public ICommand GetAnimeFigureCommand { get; }
        private bool CanGetAnimeFigureExecute(object p) => true;

        private void OnGetAnimeFigureExecuted(object p)
        {
            var url = $"{rest_url}?Id={figureId}";
            WebRequest reqGET = WebRequest.Create(url);
            reqGET.Method = "GET";
            WebResponse resp = reqGET.GetResponse();
            Stream stream = resp.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string s = sr.ReadToEnd();
        }
        #endregion

        #region CreateAnimeFigure
        public ICommand CreateAnimeFigureCommand { get; }
        private bool CanCreateAnimeFigureCommandExecute(object p) => true;

        private void OnCreateAnimeFigureCommandExecuted(object p)
        {
            var url = $"{rest_url}?Name={figureName}&Url={figureUrl}";
            WebRequest reqGET = WebRequest.Create(url);
            reqGET.Method = "GET";
            WebResponse resp = reqGET.GetResponse();
            Stream stream = resp.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string s = sr.ReadToEnd();
        }
        #endregion

        #region UpdateAnimeFigure
        public ICommand UpdateAnimeFigureCommand { get; }
        private bool CanUpdateAnimeFigureCommandExecute(object p) => true;

        private void OnUpdateAnimeFigureCommandExecuted(object p)
        {
            var url = $"{rest_url}?Id={figureId}&Name={figureName}&Url={figureUrl}";
            WebRequest reqGET = WebRequest.Create(url);
            reqGET.Method = "GET";
            WebResponse resp = reqGET.GetResponse();
            Stream stream = resp.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string s = sr.ReadToEnd();
        }
        #endregion

        #region DeleteAnimeFigure
        public ICommand DeleteAnimeFigureCommand { get; }
        private bool CanDeleteAnimeFigureExecute(object p) => true;

        private void OnDeleteAnimeFigureExecuted(object p)
        {
            var url = $"{rest_url}?Id={figureId}";
            WebRequest reqGET = WebRequest.Create(url);
            reqGET.Method = "GET";
            WebResponse resp = reqGET.GetResponse();
            Stream stream = resp.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string s = sr.ReadToEnd();
        }
        #endregion

        #endregion

        #endregion

    }
}
