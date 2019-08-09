# -*- coding:utf-8 -*-

import json

html_doc = """
<html><head><title>The Dormouse's story</title></head>
<body>
<p class="title"><b>The Dormouse's story</b></p>
<p class="story">Once upon a time there were three little sisters; and their names were
<p class="story">...</p>
"""

# 基本使用：容错处理,文档的容错能力指的是在html代码不完整的情况下,使用该模块可以识别该错误。
# 使用BeautifulSoup解析上述代码,能够得到一个 BeautifulSoup 的对象,并能按照标准的缩进格式的结构输出
#from bs4 import BeautifulSoup
#soup = BeautifulSoup(html_doc,'lxml')  # 具有容错功能
#res = soup.prettify()  # 处理好缩进，结构化显示
#print(res)


#遍历文档树：即直接通过标签名字选择，特点是选择速度快，但如果存在多个相同的标签则只返回第一个
html_doc = """
<html><head><title>The Dormouse's story</title></head>
<body>
<p id="my p" class="title"><b id="bbb" class="boldest">The Dormouse's story</b></p>

<p class="story">Once upon a time there were three little sisters; and their names were
<a href="http://example.com/elsie" class="sister" id="link1">Elsie</a>
<a href="http://example.com/lacie" class="sister" id="link2">Lacie</a> and
<a href="http://example.com/tillie" class="sister" id="link3">Tillie</a>;
and they lived at the bottom of a well.</p>

<p class="story">...</p>
"""


#1、用法
from bs4 import BeautifulSoup
soup = BeautifulSoup(html_doc,"lxml")
# soup=BeautifulSoup(open('a.html'),'lxml')

#print(soup.p) # 存在多个相同的标签则只返回第一个
#print(type(soup.a))#查看返回类型<class 'bs4.element.Tag'>
# print(soup.name) # [document] soup 对象本身比较特殊，它的 name 即为 [document]

#2、获取标签的名称
#print(soup.p.name) ## p

#3、获取标签的属性
# print(soup.p.attrs) # {'id': 'my p', 'class': ['title']}
# print(soup.p['class']) # ['title']

#4、获取标签的内容
#print(soup.p.string) #The Dormouse's story  p下的文本只有一个时，取到，否则为None
#print(soup.p.strings) #拿到一个生成器对象, 取到p下所有的文本内容
#for line in soup.p.strings:
#    print(line)

#print(soup.p.text) #取到p下所有的文本内容(不包括html元素)

#for line in soup.stripped_strings:#去掉空白  获取所有文本内容
#    print(line)

#5、嵌套选择
#print(soup.head.title.string)
#print(soup.body.a.string)

#6、子节点、子孙节点
#print(soup.p.contents) ##p下所有子节点
#print(soup.p.children) #得到一个迭代器,包含p下所有子节点

#for i,child in enumerate(soup.p.children):
#    print(i,child)

#7、父节点、祖先节点
#print(soup.a.parent) #获取a标签的父节点 (完整的父节点内容)
#print(soup.a.parents)#找到a标签所有的祖先节点，父亲的父亲，父亲的父亲的父亲... (返回对象)

#for line in soup.a.parents:
#    print(line)

#8、兄弟节点
#print(soup.a.next_sibling) #下一个兄弟
#print(soup.a.previous_sibling)##上一个兄弟

#print(list(soup.a.next_siblings)) #下面的兄弟们=>生成器对象
#print(list(soup.a.previous_siblings))#上面的兄弟们=>生成器对象


html_doc = '''<html><head><title>The Dormouse's story</title></head>
<body>
<p class="title"><b>The Dormouse's story</b></p>
<p class="title"><b>$75</b></p>
<p id="meiyuan">啦啦啦啦啦啦</p>

<p class="story">Once upon a time there were three little sisters; and their names were
<a href="http://example.com/elsie" class="sister" id="link1">Elsie</a>,
<a href="http://example.com/lacie" class="sister" id="link2">Lacie</a> and
<a href="http://example.com/tillie" class="sister" id="link3">Tillie</a>;
and they lived at the bottom of a well.</p>'''
soup = BeautifulSoup(html_doc,'lxml')
#1、字符串：特点：是一种完全匹配的
#print(soup.find_all(name="a"))  #找到所有的a标签
#print(soup.find_all(name="a aa"))  #找不到，会打印一个[]
#print(soup.find_all(attrs={"class":"sister"}))
#print(soup.find_all(text="The Dormouse's story"))  #按照文本来找
#print(soup.find_all(name="b",text="The Dormouse's story"))  #找标签名是b，并且文本是The Dormouse's story
#print(soup.p.find(name="b").text)  #第一个p标签的b里面的文本
#print(soup.find_all(name="p",attrs={"class":"story"}))  #找到标签名是p,属性名是class,
#print(soup.find(name="p",attrs={"class":"story"}).find_all(name="a")[2])  #找到标签名是p,属性名是class的第二个a标签

# 2、正则
import re
#print(soup.find_all(name=re.compile("^b")))  #找b开头的的标签
#print(soup.find_all(attrs={"id":re.compile("link")}))  #找到id属性是link的
#print(soup.find_all(text=re.compile(r"\$")))  #找带有$价钱的文本

# # 3、列表：如果传入列表参数,Beautiful Soup会将与列表中任一元素匹配的内容返回.
#print(soup.find_all(name=["a",re.compile("^b")]))  #找a标签或者b标签开头的所有的标签
#print(soup.find_all(text=["$",]))  #找不到
#print(soup.find_all(text=[re.compile(r"\$")]))  #['$75']
#print(soup.find_all(text=["a",re.compile(r"\$")]))

# # 4、True：可以匹配任何值
#print(soup.find_all(name=True))  #找到所有标签的标签名
#print(soup.find_all(attrs={"id":True}))#找到只要有id属性的
#print(soup.find_all(name="p",attrs={"id":True}))# 找到有id属性的p标签

# 5、方法：如果没有合适过滤器,那么还可以定义一个方法,方法只接受一个元素参数 ,如果这个方法返回 True 表示当前元素匹配并且被找到,如果不是则反回 False
#
# # 有class属性没有id属性的
def has_class_not_id(tag):
    return tag.has_attr('class') and not tag.has_attr('id')
    # return tag.has_attr('id') and not tag.has_attr('class')

    # return tag.name =="a" and tag.has_attr("class") and not tag.has_attr("id")
# #     #只找a标签
print(soup.find_all(has_class_not_id))  #默认是按照标签来找的