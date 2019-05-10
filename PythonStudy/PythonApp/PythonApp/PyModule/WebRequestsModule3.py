# -*- coding:utf-8 -*-

#session的使用

import re
import requests
import urllib3
import http.cookiejar as cookielib
import ssl

# 解决某些环境下报<urlopen error [SSL: CERTIFICATE_VERIFY_FAILED] certificate verify failed
ssl._create_default_https_context=ssl._create_unverified_context
urllib3.disable_warnings() #关闭警告

headers = {
        "User-Agent":"Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.108 Safari/537.36",
}

loginUrl = 'https://github.com/login'
postUrl = 'https://github.com/session'
profileUrl = 'https://github.com/settings/emails'
email='chinajianshen'
password='leilovena225515'

session=requests.session() # 包括了cookies信息

# 生成 github_cookie文件
session.cookies=cookielib.LWPCookieJar(filename='github_cookie')

# 获取authenticity_token
def get_token():
    response = session.get(loginUrl,headers=headers,verify=False)
    html=response.text
    authenticity_token=re.findall(r'input type="hidden" name="authenticity_token" value="(.*?)" />',html)
    print(authenticity_token)
    return authenticity_token

# 登陆表单提交参数
def post_account(email,password):
    post_data={
         'commit': 'Sign in',
          'utf8':'✓',
          'authenticity_token': get_token(),
          'login': email,
          'password': password
      }

    response=session.post(postUrl,data=post_data,verify=False,headers=headers)
    print(response.status_code)
    # 保存cookies
    session.cookies.save()

def load_cookie():
    try:
        session.cookies.load(ignore_discard=True)
        print('cookie 获取成功')
    except:
        print('cookie 获取不成功')

# 判断是否登陆成功
def isLogin():
    load_cookie()
    response=session.get(profileUrl,headers=headers)
    return email in response.text

if __name__=="__main__":
     # 输入自己email账号和密码
     post_account(email,password)
     # 验证是否登陆成功
     if (isLogin()):
         print("登录成功")
     else:
         print("登录失败")