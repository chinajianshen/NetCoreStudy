
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

tuple1 = ('1',2,'3','1','5')
list1 = list(tuple1)
print('list1=',list1)
print("list1.count('1')=",list1.count('1')) #注意数据类型
print("list1.pop()=",list1.pop())

print('================================')

dict1 = {'name':'runoob',"age":7,'class':'first'}
print(dict1)

print('================================')

x=True
country_counter={}
def addone(country):
    if (country in country_counter):
        country_counter[country]+=1
    else:
        country_counter[country]=1

addone('china')
addone('china')
addone('japan')
print(country_counter)

print('================================')
set1 = {'google','runoob','taobao',1,2}
print(set1)
print('pop1:',set1.pop())
print('pop2:',set1.pop())

print('================================')
"""
age=int(input('请输入年龄：'))
print("")
if age<0:
    print("错误")
elif age==1:
     print("相当于14")
elif age==2:
     print("相当于22")

input("点击enter退出")
"""

print('================================')

'''

s='Hello,Runoob'
b=str(s)#在文件中执行时出错 在命令行方式时正确
print(b)
'''

x=10*3.25
y=200*200
s='x的值为：'+repr(x)+',y的值为：'+repr(y)+'...'
print(s)

print('================================')
table = {1:'Google',2:'zh',3:'zhongguo'}
for k,v in table.items():
    print('{0:10}==>{1:10}'.format(k,v))
print()

print('1:{0[1]};2:{0[2]};3:{0[3]}'.format(table))

table = {'Google': 1, 'Runoob': 2, 'Taobao': 3}
print('Runoob: {0[Runoob]:d}; Google: {0[Google]:d}; Taobao: {0[Taobao]:d}'.format(table))
print('Runoob: {0[Runoob]}; Google: {0[Google]}; Taobao: {0[Taobao]}'.format(table))

print('================================')

'''
try:
    x=int(input("Please enter a number:"))
except ValueError:
     print("Oops!  That was no valid number.  Try again")
'''
print('================================')
try:
   f=open('myfile.txt')
   s=f.readline()
   i=int(s.strip())
except OSError as err:
    print('OS Error:{0}'.format(err))
except ValueError:
     print("Could not convert data to an integer.")
except:
     print("Unexpected error:", sys.exc_info()[0])
     raise
else:
    f.close()

print('================================')
for arg in sys.argv[1:]:
    try:
        print(arg)
        f=open(arg,'r')
    except IOError:
         print('cannot open', arg)
    else:
        print(arg, 'has', len(f.readlines()), 'lines')
        f.close()

print('================================')
from PyModule import CustomError
try:
    raise CustomError.MyError(2*2)
except CustomError.MyError as e:
   print('My exception occurred, value:', e.value)
finally:
    print('finally最后一定执行')

try:
    raise CustomError.InputError(5*2,'ERROR')
except CustomError.InputError as e:
   print('My exception occurred, value:', e.message,e.expression)
finally:
    print('finally')

print('================================')





