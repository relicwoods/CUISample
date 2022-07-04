// See https://aka.ms/new-console-template for more information

using System;
using NLog;
namespace CUISample // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        /// <summary>
        /// 
        /// </summary>
        static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// 引数解析を行うCommandLineUtilsに処理を渡す。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
    {
            var user = Environment.GetEnvironmentVariable("BASIC_AUTH_USER_NAME");
            var pass = Environment.GetEnvironmentVariable("BASIC_AUTH_PASSWORD");
            var googleCredential = Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS");
            var jobName = Environment.GetEnvironmentVariable("AWS_JOB_QUEUE_NAME");
            var jobRoleARN = Environment.GetEnvironmentVariable("AWS_JOB_EXECUTE_ROLE_ARN");


      
            if(user == null || user == "" ||
               pass == null || pass == "" ||
               googleCredential == null || googleCredential == "" ||
               jobName == null || jobName == "" ||
               jobRoleARN == null || jobRoleARN == "")
            {
                logger.Error("環境変数が適切に設定されていません");

                logger.Error("BASIC_AUTH_USER_NAME:{0}", user);
                logger.Error("BASIC_AUTH_PASSWORD:{0}", pass);
                logger.Error("GOOGLE_APPLICATION_CREDENTIALS:{0}", googleCredential);
                logger.Error("AWS_JOB_QUEUE_NAME:{0}", jobName);
                logger.Error("AWS_JOB_EXECUTE_ROLE_ARN:{0}", jobRoleARN);

                Environment.Exit(0);
            }

            //Add Applcation code


        }

    }
}




