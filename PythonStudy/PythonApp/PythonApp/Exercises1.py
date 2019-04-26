# Fibonacci series: 斐波纳契数列
# 两个元素的总和确定了下一个数

a,b=0,1
while b<10:
    #print(b)
    print(b,end=',')
    a,b=b,a+b

print("---------------斐波纳契数列---------------")

n=100
sum=0
counter=1
while counter <= n:
    sum =sum+counter
    counter+=1

print("1到%d之和为：%d" %(n,sum))

print("---------------1+2+..+100---------------")

count=0
while count<5:
    print(count,"小于5")
    count+=1
else:
    print(count,'大于或等于5')

print('----------------------------------------')

for i in range(0,10,2):
    print(i,end=' ')

print('----------------------------------------')

a = ['Google', 'Baidu', 'Runoob', 'Taobao', 'QQ']
for i in range(len(a)):
    print(a[i],end='  ')

print('----------------------------------------')

for n in range(2,10):
    for x in range(2,n):
        if n%x==0:
            print(n,'等于',x,'*',n//x)
            break
    else:
            print(n,'是质数')


print('----------------------------------------')
list =[1,2,3,4]
it = iter(list) #创建迭代器
print(next(it)) #输出迭代器的下一个元素
print(next(it))
print('--')
print(next(it))
print('----------------------------------------')


import sys
list=[1,2,3,4]
it = iter(list)
while True:
    try:
        print(next(it))
    except StopIteration:
        #sys.exit()
        #print("异常")
        break

print('----------------------------------------')
class MyNumbers:
    def __iter__(self):
       self.a=1
       return self

    def __next__(self):
        if self.a<4:
             x=self.a
             self.a+=1
             return x
        else:
             #raise StopIteration
             pass
       

myclass = MyNumbers()
myiter=iter(myclass)
print(next(myiter))
print(next(myiter))
print(next(myiter))
print(next(myiter))
print('-----------------类中实现迭代器-----------------------')

#import sys
'''
def fibonacci(n): # 生成器函数 - 斐波那契
    a, b, counter = 0, 1, 0
    while True:
        if (counter > n): 
            return
        yield a
        a, b = b, a + b
        counter += 1
f = fibonacci(10) # f 是一个迭代器，由生成器返回生成
 
while True:
    try:
        print (next(f), end=" ")
    except StopIteration:
        #sys.exit()
        pass
'''

print('----------------------------------------')
def changeme(mylist):
    mylist.append([1,2,3,4])
    print('函数内取值：',mylist)
    return

mylist= [10,20,30]
changeme(mylist)
print("函数外取值：",mylist)


print('----------------------------------------')
def printinfo(arg1,*vartuple):
    "打印任何传入的参数"
    print ("输出: ")
    print (arg1)
    for item in vartuple:
        print(item)
    #print (vartuple)

printinfo(70,60,50)
printinfo(70)

print('----------------------------------------')

sum = lambda arg1,arg2:arg1+arg2
print(sum(10,20))
print(sum(arg1=5,arg2=10))

print('----------------------------------------')
num=1
def fun1():
    global num #需要使用 global 关键字声明
    print(num)
    num=123
    print(num)

fun1()
print(num)

print('----------------------------------------')
def outer():
    num=10
    def inner():
        nonlocal num #nonlocal关键字声明
        num=100
        print(num)
    inner()
    print(num)

outer() # 100 100

print('----------------------------------------')

from collections import deque
queue=deque(['eric','john','michael'])
print(queue)

matrix=[
    [1,2,3,4],
    [5,6,7,8],
    [9,10,11,12]
       ]
print(matrix)
newmatrix=[[row[i] for row in matrix] for i in range(4)]
print(newmatrix)

#第二种方法
transposed=[]
for i in range(4):
    transposed.append([row[i] for row in matrix])
print(transposed)
print('---------------3X4的矩阵列表转换为4X3列表-------------------------')

knights = {'gallahad':'the pure','robin':'the brave'}
for k,v in knights.items():
    print(k,v)

for a in knights:
    print(a)

print('----------------------------------------')
for i,v in enumerate(['tic','tac','toe']):
    print(i,":",v)

print('----------------------------------------')
questions = ['name','quest','favorite color']
answers=['lancelot','the holy grail','blue']
for q,a in zip(questions,answers):
    print('What is your {0}? It is {1}.'.format(q,a))

print('----------------------------------------')
for i in reversed(range(1,10,2)):
    print(i)
print('----------------------------------------')
basket = ['apple','orange','apple','pear','orange','banana']
for f in sorted(set(basket)):
    print(f)
print()
for q in sorted(basket):
    print(q)