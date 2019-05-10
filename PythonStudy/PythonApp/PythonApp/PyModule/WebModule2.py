#-*- coding: utf-8 -*-
import urllib3
import requests
import urllib

'''
Urllib3是一个功能强大，条理清晰，用于HTTP客户端的Python库，许多Python的原生系统已经开始使用urllib3。Urllib3提供了很多python标准库里所没有的重要特性：

1.线程安全
2.连接池
3.客户端SSL/TLS验证
4.文件分部编码上传
5.协助处理重复请求和HTTP重定位
6.支持压缩编码
7.支持HTTP和SOCKS代理
'''

#  忽略警告：InsecureRequestWarning: Unverified HTTPS request is being made. Adding certificate verification is strongly advised.
requests.packages.urllib3.disable_warnings()

# 一个PoolManager实例来生成请求, 由该实例对象处理与线程池的连接以及线程安全的所有细节
http=urllib3.PoolManager()

header = {
        'User-Agent': 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.108 Safari/537.36'
    }


# 通过request()方法创建一个请求：
#r=http.request("GET",'http://cuiqingcai.com/')
#print(r.status)

# 获得html源码,utf-8解码
#print(r.data.decode())

#header={'User-Agent': 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.108 Safari/537.36'}
#r=http.request('GET',"https://www.baidu.com/s?",fields={'wd':'hello'},headers=header)
#print(r.status)
#print(r.data.decode())


#你还可以通过request()方法向请求(request)中添加一些其他信息，
#r=http.request("POST",'http://httpbin.org/post',fields={'hello':'world'},headers=header)
#print(r.data.decode())

print('-------------------------------------------------------------------------')
#encode_arg = urllib.parse.urlencode({'arg': '我的'})
#print(encode_arg.encode())
#r = http.request('POST',
#                 'http://httpbin.org/post?'+encode_arg,
#                 headers=header)
# unicode解码
#print(r.data.decode('unicode_escape'))

#发送json数据 JSON:在发起请求时,可以通过定义body 参数并定义headers的Content-Type参数来发送一个已经过编译的JSON数据：
#import json
#data={'attribute':'value'}
#encode_data=json.dumps(data).encode()

#r=http.request("POST",
#               'http://httpbin.org/post',
#               body=encode_data,
#               headers={'Content-Type':'application/json'})
#print(r.data.decode("unicode_escape"))

#上传文件
#使用multipart/form-data编码方式上传文件,可以使用和传入Form data数据一样的方法进行,并将文件定义为一个元组的形式　　　　　(file_name,file_data):
#import os
#print(os.getcwd())
#if os.path.exists('1.txt'):
#    print('存在')
#else:
#    print('不存在')

#with open('1.txt','r+') as f:
#    file_read=f.read()

#r=http.request('POST',
#               'http://httpbin.org/post',
#               fields={'filefield':('1.txt',file_read,'text/plain')})
#print(r.data.decode())

#二进制文件
#with open('websocket.jpg','rb') as f2:
#    binary_read=f2.read()

#http=urllib3.PoolManager()
#r=http.request("POST",
#               'http://httpbin.org/post',
#               body=binary_read,
#               headers={'Content-Type':'image/jpeg'})
# print(json.loads(r.data.decode('utf-8'))['data'] )
#print(r.data.decode('utf-8'))

#重定向
#import requests

#r=requests.get('http://github.com')
#print(r.url)
#print(r.status_code)
#print(r.history)


#r=requests.get('http://github.com',allow_redirects=False) #不允许重定向
#print(r.url)#http://github.com
#print(r.status_code) #301
#print(r.history)#[]

#print('-----------------------------')
#r=requests.head('http://github.com',allow_redirects=True)
#print(r.url)#http://github.com
#print(r.status_code) #301
#print(r.history)#[<Response [301]>]

#高级认证
#证书验证(大部分网站都是https)
import requests
from urllib import parse

res = requests.get('https://www.12306.cn')

print(res.encoding)
print(res.headers["Content-Type"])
print(res.status_code)
#res.encoding='GBK'
#print(res.text)
#print(parse.unquote(res.text))

