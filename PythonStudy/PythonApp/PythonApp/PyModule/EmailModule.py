import smtplib
from email.mime.text import MIMEText
from email.header import Header

#QQ邮箱的服务器端口号为，收件服务器（IMAP): imap.qq.com 端口号是：993，发件服务器（SMTP):smtp.qq.com 端口号是：465

sender='281349838@qq.com'
receive=['361631053@qq.com']

# 三个参数：第一个为文本内容，第二个 plain 设置文本格式，第三个 utf-8 设置编码
message =MIMEText('Python 邮件发送测试...','plain','utf-8')
message['From']=Header('菜鸟教程','utf-8') # 发送者
message['To']=Header("测试", 'utf-8') # 接收者
authcode='lyibtgwckiqtbhaf' #发送方邮箱的授权码

subject='Python SMTP 邮件测试'
message['Subject']=Header(subject,'utf-8')

try:
    smtpObj=smtplib.SMTP_SSL('smtp.qq.com',465)
    smtpObj.login(sender,authcode)
    smtpObj.sendmail(sender,receive,message.as_string())
    print ("邮件发送成功")
except smtplib.SMTPException as e:
    print("Error,邮件发送失败：%s",e)