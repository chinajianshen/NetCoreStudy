#-*- coding:utf-8 -*- 

import json
import hashlib as hasher
import requests
import random
import time
import ssl
import urllib3

# 解决某些环境下报<urlopen error [SSL: CERTIFICATE_VERIFY_FAILED] certificate verify failed
ssl._create_default_https_context=ssl._create_unverified_context
urllib3.disable_warnings()#关闭警告

start_url='http://fanyi.youdao.com/'
post_url='http://fanyi.youdao.com/translate_o?smartresult=dict&smartresult=rule'

headers = {
       "User-Agent":"Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.108 Safari/537.36",
       "Referer":"http://fanyi.youdao.com/"
    }

def get_cookies(url):
    return requests.get(url).cookies

#得到js加密串
def get_JSKey(r_word):
    salt=int(time.time()*1000)+random.randint(0,9)
    md=hasher.md5()
    md5_str=("fanyideskweb"+r_word+str(salt)+"@6f#X3=cCuncYssPsuRUE").encode('utf-8')  #@6f#X3=cCuncYssPsuRUE   ebSeFb%=XZ%T[KZ)c(sy!
    md.update(md5_str)
    sign=md.hexdigest()
    return {"salt":salt,"sign":sign}

def get_content(r_word,url,cookies,js_key):
     post_data = {
        "i": r_word,
        "from": "AUTO",
        "to": "AUTO",
        "smartresult": "dict",
        "client": "fanyideskweb",
        "salt": js_key["salt"],  # salt
        "sign":js_key["sign"],  # sign
        "doctype": "json",
        "version": "2.1",
        "keyfrom": "fanyi.web",
        "action": "FY_BY_REALTIME",
        "typoResult": "false"
    }
     response=requests.post(url,headers=headers,data=post_data,cookies=cookies)
     json_str=response.json()
     print(json_str)


if __name__ == '__main__':
    r_word=input('please input the word you want to translate :')
    cookies = get_cookies(start_url)
    print('cookies=>', cookies)   
    js_key = get_JSKey(r_word)
    print("js_key=>",js_key)
    get_content(r_word,post_url,cookies,js_key)  # 得到请求内容后返回的json