#访问 互联网
#有几个模块用于访问互联网以及处理网络通信协议。其中最简单的两个是用于处理从 urls 接收的数据的 urllib.request 以及用于发送电子邮件的 smtplib:
from urllib.request import urlopen
for line in urlopen('http://www.baidu.com'):
    line = line.decode('utf-8')
    print(line)
    break

print('================================================')

'''
import smtplib
server=smtplib.SMTP('https://mail.qq.com')
server.sendmail('281349838@qq.com','281349838@qq.com','111111111')
server.quit()
'''


