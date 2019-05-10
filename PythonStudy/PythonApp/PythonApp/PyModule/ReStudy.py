import re

#re.match只匹配字符串的开始，如果字符串开始不符合正则表达式，则匹配失败，函数返回None；而re.search匹配整个字符串，直到找到一个匹配。


print(re.match('www','www.runoob.com').span()) # 在起始位置匹配 (0,3)
print(re.match('www','www.runoob.com').group()) # www
print(re.match('www','www.runoob.com')) # 不在起始位置匹配 <_sre.SRE_Match object; span=(0, 3), match='www'>

print(re.match('com', 'www.runoob.com')) # 不在起始位置匹配 None
#print(re.match('com', 'www.runoob.com').group()) #不在超始位置匹配 None  .group()报错
#print(re.match('com', 'www.runoob.com').span()) # 不在起始位置匹配 .span()报错

print('========================================================')
line = "Cats are smarter than dogs"
# .* 表示任意匹配除换行符（\n、\r）之外的任何单个或多个字符
matchObj = re.match( r'(.*) are (.*?) .*', line, re.M|re.I)
print(matchObj.groups()) #('Cats', 'smarter')

if (matchObj):
     print("matchObj.group() : ", matchObj.group())
     print("matchObj.group(1) : ", matchObj.group(1))
     print("matchObj.group(2) : ", matchObj.group(2))
else:
    print('No match!!')

#re.search 扫描整个字符串并返回第一个成功的匹配。
print(re.search('www','www.runoob.com').span()) #在起始位置匹配(0,3)
print(re.search("com","www.runoob.com").span())  # 不在起始位置匹配(11,14)

print('========================================================')
line = "Cats are smarter than dogs"
searchObj=re.search(r'(.*?) are (.*?) .*',line,re.M|re.I)
if searchObj:
   print ("searchObj.group() : ", searchObj.group())
   print ("searchObj.group(1) : ", searchObj.group(1))
   print ("searchObj.group(2) : ", searchObj.group(2))
else:
   print ("Nothing found!!")

print('========================================================')
line = "Cats are smarter than dogs"
matchObj = re.match(r'dogs',line,re.M | re.I)
if matchObj:
   print ("match --> matchObj.group() : ", matchObj.group())
else:
   print ("No match!!")


matchObj=re.search(r'dogs',line,re.M |re.I)
if matchObj:
   print ("search --> matchObj.group() : ", matchObj.group())
else:
   print ("No match!!")

#检索和替换
#Python 的re模块提供了re.sub用于替换字符串中的匹配项
phone='2004-959-559 # 这是一个电话号码'
# 删除注释
num=re.sub(r'#.*$',"",phone)
print('电话号码:',num)

# 移除非数字的内容
num=re.sub(r'\D',"",phone)
print('电话号码:',num)

#repl 参数是一个函数
def double(matched):
    value=int(matched.group('value'))
    return str(value*2)

s='A23G4HFD567'
print(re.sub('(?P<value>\d+)',double,s))

print('========================================================')
#compile 函数用于编译正则表达式，生成一个正则表达式（ Pattern ）对象，供 match() 和 search() 这两个函数使用
pattern = re.compile(r'([a-z]+) ([a-z]+)',re.I)
m=pattern.match('Hello World Wide Web')
print(m)
print(m.span(0))
print(m.span(1))
print(m.span(2))

print(m.group())
print(m.group(0))
print(m.group(1))
print(m.group(2))

print(m.groups())
print(len(m.groups()))

print('========================================================')
#indall
#在字符串中找到正则表达式所匹配的所有子串，并返回一个列表，如果没有找到匹配的，则返回空列表。
#注意： match 和 search 是匹配一次 findall 匹配所有。
pattern=re.compile(r'\d+')
print(pattern.findall('runoob 123 google 456')) #['123', '456']
print(pattern.findall('run88oob123google456',0,10))#['88', '12']

#re.finditer 在字符串中找到正则表达式所匹配的所有子串，并把它们作为一个迭代器返回

it =re.finditer(r'\d+','12a32bc43jf3')
for match in it:
    print(match.group())

print('========================================================')
print(re.split('\W+','runoob, runoob, runoob.'))#['runoob', 'runoob', 'runoob', '']
print(re.split('(\W+)',' runoob,runoob,runoob.'))#['', ' ', 'runoob', ', ', 'runoob', ', ', 'runoob', '.', '']
print( re.split('\W+', ' runoob,runoob,runoob.', 1) )#['', 'runoob,runoob,runoob.']