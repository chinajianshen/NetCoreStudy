#-*- coding:utf-8 -*-

import requests
from bs4 import BeautifulSoup
from requests.exceptions import *
import mysql.connector
import time
import re

'''
爬取/www.qiushibaike.com
热门话题的用户信息，并保存到mysql数据库中
'''

start_url="https://www.qiushibaike.com"

class qsbk(object):

    def __init__(self):
        self.session=requests.session()   # 包括了cookies信息
        self.headers={
              "User-Agent": "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.108 Safari/537.36",
            }

        self.conn=mysql.connector.connect(host="localhost", user='root',password='sa.',database='spiderdbinfo')
        self.cursor=self.conn.cursor()

    def get_response(self,url):
        try:
            response=self.session.get(url,headers=self.headers,timeout=5)
            if response.status_code ==200:
                return response.text
            else:
                time.sleep(1)
                return self.get_response(url)
        except ReadTimeout:
            print('ReadTimeout')
        except ConnectionError:
            print('ConnectionError')
        except RequestException:
            print('Error')

    # 解析用户url /users/24057284/
    def parse_userurl(self,text):
        soup=BeautifulSoup(text,"lxml")
        #print(soup.prettify())
        author_div=soup.find(name="div",class_="recommend-article")
        author_li=author_div.find_all(name="li") # author clearfix
        url_li=[]
        for item in author_li:
            if item.find("a",class_="recmd-user") != None:
                url=item.find("a",class_="recmd-user").attrs['href']
                url_li.append(url)
        return url_li

    #解析用户数据
    def parse_userdata(self,text):
        soup=BeautifulSoup(text,"lxml")

        if '当前用户已关闭糗百个人动态' in text:
             print('当前用户已关闭')
             return None
        else:
            username = soup.find('h2').text
            result = soup.findAll('div', class_='user-statis')

            number = result[0].find_all('li')[0].text
            attentions = result[0].find_all('li')[1].text
            comments = result[0].find_all('li')[3].text

            constellation = result[1].find_all('li')[1].text
            occupation = result[1].find_all('li')[2].text
            address = result[1].find_all('li')[3].text

            return username, number, attentions, comments, constellation, occupation, address

     # 保存到数据库中
    def save_mydata(self, data):
        # print (data)
        if data != None:
            sql = 'insert into qsbk_user (username,num,attentions,comments,constellation,occupation,address) VALUES (%s,%s,%s,%s,%s,%s,%s)'
            li = [item.split(":")[-1] for item in data]
            # print('data=',li) # data= ['绠纱猫猫', '16', '3', '297', '天蝎座', '家里蹲', '湖南 · 长沙']
            try:
                self.cursor.execute(sql, tuple(li))
                self.conn.commit()
            except Exception as e:
                print(e)


    def main(self,url):
        response =self.get_response(url)
        try:
            url_li=self.parse_userurl(response)
            for item in url_li:
                user_detail_url=url+item
                data = self.parse_userdata(self.get_response(user_detail_url))
                self.save_mydata(data)
        except IndexError as e:
            print(e)
        except Exception as e:
            print(e)

if __name__ == "__main__":
    qsbk().main(start_url)

