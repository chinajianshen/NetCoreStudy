'''
#GET请求
HTTP默认的请求方法就是GET
     * 没有请求体
     * 数据必须在1K之内！
     * GET请求数据会暴露在浏览器的地址栏中

GET请求常用的操作：
       1. 在浏览器的地址栏中直接给出URL，那么就一定是GET请求
       2. 点击页面上的超链接也一定是GET请求
       3. 提交表单时，表单默认使用GET请求，但可以设置为POST


#POST请求
(1). 数据不会出现在地址栏中
(2). 数据的大小没有上限
(3). 有请求体
(4). 请求体中如果存在中文，会使用URL编码！
#！！！requests.post()用法与requests.get()完全一致，特殊的是requests.post()有一个data参数，用来存放请求体数据
'''

'''
一 目标站点分析
    浏览器输入https://github.com/login
    然后输入错误的账号密码，通过Fiddle抓包
    发现登录行为是post提交到：https://github.com/session
    而且请求头包含cookie
    而且请求体包含：
        commit:Sign in
        utf8:✓
        authenticity_token:lbI8IJCwGslZS8qJPnof5e7ZkCoSoMn6jmDTsL1r/m06NLyIbw7vCrpwrFAPzHMep3Tmf/TSJVoXWrvDZaVwxQ==
        login:908099665@qq.com
        password:123

二 流程分析
    先GET：https://github.com/login拿到初始cookie与authenticity_token
    返回POST：https://github.com/session， 带上初始cookie，带上请求体（authenticity_token，用户名，密码等）
    最后拿到登录cookie

    ps：如果密码时密文形式，则可以先输错账号，输对密码，然后到浏览器中拿到加密后的密码，github的密码是明文
'''

#发送post请求，登录github
#-*- coding: utf-8 -*-

import re
import requests
import http.cookiejar as cookielib
from requests.packages import urllib3
import ssl
# 解决某些环境下报<urlopen error [SSL: CERTIFICATE_VERIFY_FAILED] certificate verify failed
ssl._create_default_https_context=ssl._create_unverified_context
urllib3.disable_warnings()# 关闭警告

headers={ "User-Agent":"Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.108 Safari/537.36" }    

loginUrl='https://github.com/login'
postUrl='https://github.com/session'

res=requests.get(loginUrl,headers=headers,verify=False)
# 获取authenticity_token
authenticity_token=re.findall(r'input type="hidden" name="authenticity_token" value="(.*?)" />',res.text)
#获取cookies
cookies=res.cookies
#print('cookies=>',cookies)
#print('authenticity_token=>',authenticity_token)

email='chinajianshen'
password='leilovena225515'
post_data={
   "commit":"Sign in",
   "utf8":"✓",
   "authenticity_token":authenticity_token,
   "login":email,
   "password":password
}
resPost=requests.post(postUrl,
                      data=post_data,
                      headers=headers,
                      verify=False,
                      cookies=cookies)
print(resPost.status_code)
print(resPost.history)  # 跳转的历史状态码
print(resPost.text)