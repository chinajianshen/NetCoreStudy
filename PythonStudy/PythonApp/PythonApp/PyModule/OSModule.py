import os
print('当前工作目录：',os.getcwd())

#os.chdir('E:\\GitSourceCode\\NetCoreStudy\\PythonStudy\\PythonApp')
#print('当前新工作目录：',os.getcwd())

'''
print(dir(os))
print(help(os))
'''


print('---------------123------------------------------')
#针对日常的文件和目录管理任务，:mod:shutil 模块提供了一个易于使用的高级接口:
#import shutil
#shutil.copyfile('data.db', 'archive.db')
#shutil.move('/build/executables', 'installdir')

#文件通配符
#glob模块提供了一个函数用于从目录通配符搜索中生成文件列表:

import glob
print(glob.glob('*.py'))

#命令行参数
#通用工具脚本经常调用命令行参数。这些命令行参数以链表形式存储于 sys 模块的 argv 变量
import sys
print(sys.argv)

print('=================================================================')
#错误输出重定向和程序终止
#sys 还有 stdin，stdout 和 stderr 属性，即使在 stdout 被重定向时，后者也可以用于显示警告和错误信息。
#a=int('aq')
sys.stderr.write('Warning, log file not found starting a new one\n')

print('=================================================================')
#字符串正则匹配
import re
re1= re.findall(r'\bf[a-z]*','which foot or hand fell fastest')
print(re1)

re2= re.sub(r'(\b[a-z]+) \1', r'\1', 'cat in the the hat')
print(re2)

re3= re.sub(r'(\bc[a-z]+) \1', r'\1', 'cat in the the hat cie')
print(re3)

print('=================================================================')

#数学
#math模块为浮点运算提供了对底层C函数库的访问:
import math
print(math.cos(math.pi/4))
print(math.log(1024,2))

print('=================================================================')
#random提供了生成随机数的工具
import random
print(random.choice(['apple','pear','banana']))

print(random.sample(range(100),10))
print(random.sample(range(100),10))

print(random.random())
print(random.randrange(6))
print(random.randrange(6))

print('=================================================================')
'''
datetime模块为日期和时间处理同时提供了简单和复杂的方法。

支持日期和时间算法的同时，实现的重点放在更有效的处理和格式化输出。

该模块还支持时区处理:
'''
from datetime import date
print(date.today())
now = date.today()
birth=date(1986,12,4)
age=now-birth
print(age)

print('=================================================================')
#数据压缩
#以下模块直接支持通用的数据打包和压缩格式：zlib，gzip，bz2，zipfile，以及 tarfile
import zlib
s=b'witch which has which witches wrist watch'
print(len(s))

t=zlib.compress(s)
print(len(t))

print(zlib.decompress(t))

print(zlib.crc32(s))

print('=================================================================')
'''
性能度量
有些用户对了解解决同一问题的不同方法之间的性能差异很感兴趣。Python 提供了一个度量工具，为这些问题提供了直接答案。

例如，使用元组封装和拆封来交换元素看起来要比使用传统的方法要诱人的多,timeit 证明了现代的方法更快一些。
'''

from timeit import Timer
print(Timer('t=a;a=b;b=t','a=1;b=2').timeit())
print(Timer('a,b=b,a','a=1;b=2').timeit())

