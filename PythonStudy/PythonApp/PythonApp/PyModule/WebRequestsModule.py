import requests
from urllib import parse

headers={
    "User-Agent": "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.108 Safari/537.36"
}

#对url进行传参
param={'wd': '中国'}
#res=requests.get('http://www.baidu.com/s?',params=param)
#print(res.url)
## url解码 ASCII --》utf8
#print(parse.unquote(res.url))
#print(parse.unquote(res.text))

## 添加headers(浏览器会识别请求头,不加可能会被拒绝访问,比如访问https://www.zhihu.com/explore)

#res=requests.get("https://www.zhihu.com/explore")
#print(res.status_code) #不加header头 返回400

# 自己定制headers
#headers={
#    "User-Agent": "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.108 Safari/537.36"
#}
#res =requests.get('https://www.zhihu.com/explore',headers=headers)
#print(res.status_code)


#GET请求->cookies
#loginUrl = 'https://github.com/login'
#res=requests.get(loginUrl,headers=headers)
#cookies = res.cookies
#print('cookies=>',cookies)

#GET请求->代理
#proxies={
#    'http':'111.47.220.67:8080',
#    'https':'111.47.220.67:8080',
#}
#response = requests.get('https://www.zhihu.com/explore',
#                      proxies= proxies,
#                      headers = headers, verify=False)

#print(response.status_code)


res=requests.get('http://www.jianshu.com')

#res属性
#print(res.text) # 文本数据str 经过转码的
#print(res.content)#原始数据字节串bytes
#print(res.status_code) #返回状态码 200
#print(res.headers)
#print(res.cookies)
#print(res.cookies.get_dict())
#print(res.cookies.items())
#print(res.url)
#print(res.history)
#print(res.encoding)

#编码问题
res=requests.get('http://www.autohome.com/news')
#print(res.headers['Content-Type']) ## 返回text/html
# 如果返回值不包括charset元素，默认返回编码为ISO-8859-1
#print(res.encoding) #返回ISO-8859-1 按ISO-8859-1方式解码text
#print(res.text) # 按ISO-8859-1方式解码text 中文乱码
#res.encoding='GBK'# 汽车之家网站返回的页面内容为gb2312编码的，而requests的默认编码为ISO-8859-1，如果不设置成gbk则中文乱码
#print(res.text)

#res=requests.get('https://www.jianshu.com',headers=headers)
#print(res.headers['Content-Type']) # 返回text/html; charset=utf-8
#print(res.encoding) #utf-8
#print(res.text)#简书返回的页面内容为utf-8编码的，在这里不用设置response.encoding = 'utf-8' 

# 解析json
#import requests
#import json

#res = requests.get('http://httpbin.org/get')
#print(res.text)
#res1=json.loads(res.text)# 以往获取方式太麻烦
#res2=res.json()# 直接获取json数据
#print(res2)
#print(res1 == res2)

# 获取二进制数据
#import requests
#res = requests.get('http://pic-bucket.nosdn.127.net/photo/0005/2018-02-26/DBIGGI954TM10005NOS.jpg')
#with open('a.jpg','wb') as f:
#    f.write(res.content) #res.text错误 不是二进制的

# stream参数:一点一点的取,比如下载视频时,如果视频100G,用response.content然后一下子写到文件中是不合理的
#res=requests.get('https://gss3.baidu.com/6LZ0ej3k1Qd3ote6lo7D0j9wehsv/tieba-smallvideo-transcode/1767502_56ec685f9c7ec542eeaf6eac93a65dc7_6fe25cd1347c_3.mp4',
#                 stream=True)
#with open('b.mp4','wb') as f:
#    # 获取二进制流(iter_content)
#    for line in res.iter_content():
#        f.write(line)


#基于POST请求
#！！！requests.post()用法与requests.get()完全一致，特殊的是requests.post()有一个data参数，用来存放请求体数据
