using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetWebApi.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace DotnetWebApi.Helper
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public async Task SendEmailiAsync(MailRequest mailRequest)
        {
            // 寄/發送人的資訊
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            // 主題
            email.Subject = mailRequest.Subject;
            //=============================================================
            //發送內容
            var builder = new BodyBuilder();
            if (mailRequest.Attachments != null) // 事處理檔案的部分
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            // 郵件訊息內容
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            //=============================================================
            //smtp的寄送方式(使用appsetting.json的資訊)
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public string MailBody(string VerificationCode)
        {
            return $@"<div width=""100%"" style=""margin: 0; background-color: #f0f2f3"">
                    <div
                        style=""margin: auto; max-width: 600px; padding-top: 50px""
                        class=""m_-4608655372151934672email-container""
                    >
                        <table
                        role=""presentation""
                        cellspacing=""0""
                        cellpadding=""0""
                        width=""100%""
                        align=""center""
                        id=""m_-4608655372151934672logoContainer""
                        style=""background: #252f3d; border-radius: 3px 3px 0 0; max-width: 600px""
                        >
                        <tbody>
                            <tr>
                            <td
                                style=""
                                background: #252f3d;
                                border-radius: 3px 3px 0 0;
                                padding: 20px 0 10px 0;
                                text-align: center;
                                ""
                            >
                                <img
                                src=""https://cdn-icons-png.flaticon.com/512/1809/1809216.png""
                                width=""80""
                                height=""80""
                                alt=""AWS logo""
                                border=""0""
                                style=""
                                    font-family: sans-serif;
                                    font-size: 15px;
                                    line-height: 140%;
                                    color: #555555;
                                ""
                                class=""CToWUd""
                                data-bit=""iit""
                                />
                            </td>
                            </tr>
                        </tbody>
                        </table>

                        <table
                        role=""presentation""
                        cellspacing=""0""
                        cellpadding=""0""
                        width=""100%""
                        align=""center""
                        id=""m_-4608655372151934672emailBodyContainer""
                        style=""border: 0px; border-bottom: 1px solid #d6d6d6; max-width: 600px""
                        >
                        <tbody>
                            <tr>
                            <td
                                style=""
                                background-color: #fff;
                                color: #444;
                                font-family: 'Amazon Ember', 'Helvetica Neue', Roboto, Arial,
                                    sans-serif;
                                font-size: 14px;
                                line-height: 140%;
                                padding: 25px 35px;
                                ""
                            >
                                <h1
                                style=""
                                    font-size: 20px;
                                    font-weight: bold;
                                    line-height: 1.3;
                                    margin: 0 0 15px 0;
                                ""
                                >
                                驗證您的電子郵件地址
                                </h1>
                                <p style=""margin: 0; padding: 0"">
                                感謝您開始新
                                <span class=""il"">Floor Blog</span>
                                帳戶的建立程序。我們欲確定這是您本人所進行的操作。<wbr />請在提示出現時輸入以下驗證碼。若您不想建立帳戶，<wbr />則可忽略此訊息。
                                </p>
                                <p style=""margin: 0; padding: 0""></p>
                            </td>
                            </tr>
                            <tr>
                            <td
                                style=""
                                background-color: #fff;
                                color: #444;
                                font-family: 'Amazon Ember', 'Helvetica Neue', Roboto, Arial,
                                    sans-serif;
                                font-size: 14px;
                                line-height: 140%;
                                padding: 25px 35px;
                                padding-top: 0;
                                text-align: center;
                                ""
                            >
                                <div style=""font-weight: bold; padding-bottom: 15px"">驗證碼</div>
                                <div
                                style=""
                                    color: #000;
                                    font-size: 36px;
                                    font-weight: bold;
                                    padding-bottom: 15px;
                                ""
                                >
                                {VerificationCode}
                                </div>
                                <div>(此驗證碼於 10 分鐘內有效)</div>
                            </td>
                            </tr>
                            <tr>
                            <td
                                style=""
                                background-color: #fff;
                                border-top: 1px solid #e0e0e0;
                                color: #777;
                                font-family: 'Amazon Ember', 'Helvetica Neue', Roboto, Arial,
                                    sans-serif;
                                font-size: 14px;
                                line-height: 140%;
                                padding: 25px 35px;
                                ""
                            >
                                <p style=""margin: 0 0 15px 0; padding: 0 0 0 0"">
                                Floor Blog 絕不會傳送電子郵件給您，要求您公開或驗證您的密碼
                                </p>
                            </td>
                            </tr>
                        </tbody>
                        </table>

                        <table
                        role=""presentation""
                        cellspacing=""0""
                        cellpadding=""0""
                        width=""100%""
                        align=""center""
                        id=""m_-4608655372151934672footer""
                        style=""max-width: 600px""
                        >
                        <tbody>
                            <tr>
                            <td
                                style=""
                                color: #777;
                                font-family: 'Amazon Ember', 'Helvetica Neue', Roboto, Arial,
                                    sans-serif;
                                font-size: 12px;
                                line-height: 16px;
                                padding: 20px 30px;
                                text-align: center;
                                ""
                            >
                                此訊息由 Floor Blog© 2022, Floor Blog, Inc. 保留所有權利。
                            </td>
                            </tr>
                        </tbody>
                        </table>
                    </div>
                    </div>
                    ";
        }
    }
}