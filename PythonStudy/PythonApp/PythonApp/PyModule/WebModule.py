#访问 互联网
#有几个模块用于访问互联网以及处理网络通信协议。其中最简单的两个是用于处理从 urls 接收的数据的 urllib.request 以及用于发送电子邮件的 smtplib:
from urllib.request import urlopen

'''
urllib是一个包含几个模块来处理请求的库。分别是：
urllib.request 发送http请求
urllib.error 处理请求过程中,出现的异常。
urllib.parse 解析url
urllib.robotparser 解析robots.txt 文件

urlopen返回对象提供一些基本方法：
read 返回文本数据
info 服务器返回的头信息
getcode 状态码
geturl 请求的url
'''

'''
for line in urlopen('http://www.baidu.com'):
    line = line.decode('utf-8')
    print(line)
    break

print('================================================')

import requests
r = requests.get('http://www.baidu.com')

print(r.text)
print(r.url)
'''

'''
for item in r:
     print(item)
'''
'''
r =requests.post('http://httpbin.org/post', data = {'key':'value'})
print(r)
print('================================================')

payload ={'key1':'value1','key2':'value2'}
r=requests.get('http://httpbin.org/get',params=payload)
print(r.url)
print(r.text)
'''

print('==========================11111111======================')

from urllib import request
import ssl
# 解决某些环境下报<urlopen error [SSL: CERTIFICATE_VERIFY_FAILED] certificate verify failed
#ssl._create_default_https_context = ssl._create_unverified_context

'''
res = request.urlopen('http://www.openbookdata.com.cn',data=None,timeout=10)
print(res.read().decode('utf-8'))
'''

'''
#如果不加上下面的这行出现会出现urllib2.HTTPError: HTTP Error 403: Forbidden错误
#主要是由于该网站禁止爬虫导致的，可以在请求加上头信息，伪装成浏览器访问User-Agent,具体的信息可以通过火狐的FireBug插件查询
#需要添加headers头信息，urlopen不支持，需要使用Request
url='https://www.jianshu.com'
headers = {'User-Agent':'Mozilla/5.0 (Windows NT 6.1; WOW64; rv:23.0) Gecko/20100101 Firefox/23.0'}
req =request.Request(url,headers=headers)
#req.add_header("User-Agent","Mozilla/5.0 (Windows NT 6.1; WOW64; rv:23.0) Gecko/20100101 Firefox/23.0") #也可以这样添加head头信息
res=request.urlopen(req)

#直接用urllib.request模块的urlopen()获取页面，page的数据格式为bytes类型，需要decode()解码，转换成str类型。
#print(res.read().decode('utf-8')) #网页内容
#print(res.read()) #bytes类型数据
#print(res.info())
#print(res.getcode())
#print(res.geturl())
#在urllib里面 判断是get请求还是post请求，就是判断是否提交了data参数
#print(req.get_method())
'''

'''
#手机模拟请求
req =request.Request('http://www.douban.com/')
req.add_header("User-Agent",'Mozilla/6.0 (iPhone; CPU iPhone OS 8_0 like Mac OS X) ' 'AppleWebKit/536.26 (KHTML, like Gecko) Version/8.0 Mobile/10A5376e Safari/8536.25')

with request.urlopen(req) as f:
    print('Status:', f.status, f.reason)
    for k,v in f.getheaders():
        print('%s: %s' % (k, v))
    print("Data:",f.read().decode('utf-8'))
'''


#Cookie的使用  用户名和密码登录
'''
import http.cookiejar,urllib.request
import time
import json

# 1 创建CookieJar对象
cookie = http.cookiejar.CookieJar()
#使用HTTPCookieProcessor创建cookie处理器，
handler=urllib.request.HTTPCookieProcessor(cookie)
#构建opener对象
opener=urllib.request.build_opener(handler)
# 将opener安装为全局
urllib.request.install_opener(opener)
req_data= urllib.parse.urlencode({'ln':'kaijuansdb','pw':'kaijuansdb','ts':time.time()}).encode('utf-8')
req = urllib.request.Request('http://www.openbookinfo.com.cn/handlers/UserController/UserLogins.ashx',data=req_data)
res = urllib.request.urlopen(req)

if res.status==200:
    result = res.read().decode('utf-8')
    print(result)
    #jsonParse = json.dumps(result) #转换成json串
    hjson = json.loads(result) #json串转换成python对象
    if hjson['state'] == 1:
        res = urllib.request.urlopen("http://www.openbookinfo.com.cn/Analyse/WeekBestseller_0_2.html")
        print(res.read().decode('utf-8'))
        print('-----------------444444444444444444444444444-------------------------------------')
        res = urllib.request.urlopen("http://www.openbookinfo.com.cn/Analyse/MarketCY_6_3.html")
        print(res.read().decode('utf-8'))
    else:
        print("登录失败")
else:
    print('请求失败')
'''
'''
urllib.error
urllib.error可以接收有urllib.request产生的异常。urllib.error中常用的有两个方法，URLError和HTTPError。URLError是OSError的一个子类，
HTTPError是URLError的一个子类，服务器上HTTP的响应会返回一个状态码，根据这个HTTP状态码，我们可以知道我们的访问是否成功。
'''

#URLError产生原因一般是:网络无法连接、服务器不存在等
'''
import urllib.error
import urllib.request

req=urllib.request.Request("http://www.usahfkjashfj.com/")
try:
    urllib.request.urlopen(req).read()
except urllib.error.URLError as e:
    print(e.reason)
else:
    print("success")
'''

'''
from urllib import request,error
 # 先捕获子类错误
try:
     res = request.urlopen('http://cuiqingcai.com/index.htm')
except error.HTTPError as e:
    print(e.reason,e.code,e.headers,sep='\n')
except error.URLError as e:
    print(e.reason)
else:
    print('Request Successfully')
'''

from urllib import request,parse
url=r'https://www.jianshu.com/collections/20f7f4031550/mark_viewed.json'
headers={
    'User-Agent': r'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36',
    'Referer': r'https://www.jianshu.com/c/20f7f4031550?utm_medium=index-collections&utm_source=desktop',
    'Connection': 'keep-alive'
}

data = {'uuid': '5a9a30b5-3259-4fa0-ab1f-be647dbeb08a',}

#Post的数据必须是bytes或者iterable of bytes，不能是str，因此需要进行encode（）编码
data = parse.urlencode(data).encode('utf-8')
print(data)

req = request.Request(url,headers=headers,data=data)
html = request.urlopen(req).read().decode('utf-8')
print(html)