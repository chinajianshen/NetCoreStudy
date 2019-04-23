
import sys

para_str = """这是一个多行字符串的实例
多行字符串可以使用制表符
TAB(\t)
也可以使用换行符 [ \n ]。
"""
print(para_str)

print('================Python import mode==========================');
print("命令行参数为：")
for i in sys.argv:
    print(i)

print('\n python路径为：',sys.path)

print('================================')


x='runoob';
sys.stdout.write(x +'\n')

a='a'
b='b'
#换行输出
print(a)
print(b)

print('-------')

#不换行输出
print(a,b)

a1=b1=c1=1
print(a1,b1,c1)

a2,b2,c2=1,2,'john'
print(a2,b2,c2)

print('================================')
var1 =1
var2=10
print(var1,var2)
del var1
print(var2)
# print(var1) #已删除 报异常


print('================================')

x='a'
y='b'
#不换行输出
print(x,end="") #end=" " 
print(y,end="")

print('================================')

def a():
    '''这是文档字符串'''

print(a.__doc__)

print('================================')

str = 'abcef'
print('%s:%s'%(str,str.capitalize()))

print('================================')