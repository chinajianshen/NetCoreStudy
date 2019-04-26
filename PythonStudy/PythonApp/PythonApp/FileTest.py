import os

'''
f=open('E:\\GitSourceCode\\NetCoreStudy\\PythonStudy\\PythonApp\\PythonApp\\tmp\\foo.txt','w')
f.write("Python 是一个非常好的语言。\n是的，的确非常好!!\n")
f.close() #必须关闭 否则文件内容没有写入成功
'''

filepath= os.path.join(os.path.abspath('.'),'tmp','foo.txt')
print(filepath)

print("os.getcwd():",os.getcwd()) #返回当前工作目录


#print('os.access():',os.access(filepath,"w"))

f=open(filepath)
str = f.read()
f.close()
print(str)

print('\n\n')
f=open(filepath)
str=f.read(8)
f.close()
print(str)

print('\n\n')
f = open(filepath,"r")
str = f.readline()
print(str)
str=f.readline()
print(str)
str=f.readline()
if (str == ""):
    print("读取到结尾")
else:
    print("未到结尾")

print("--------------------------------------")
f=open(filepath,"r")

for line in f:
    print(line,end='')

f.close()

print('-----------------------')

'''
filepath= os.path.join(os.path.abspath('.'),'tmp','foo1.txt')
import pickle
data1 = {'a': [1, 2.0, 3, 4+6j],
         'b': ('string', u'Unicode string'),
         'c': None}

selfref_list = [1, 2, 3]
selfref_list.append(selfref_list)
output = open(filepath,'wb')
pickle.dump(data1,output)
pickle.dump(selfref_list,output,-1)
output.close()

print('-----------------------')
import pprint,pickle
f = open(filepath,'rb')
data1=pickle.load(f)
pprint.pprint(data1)

data2=pickle.load(f)
pprint.pprint(data2)
f.close()
'''
