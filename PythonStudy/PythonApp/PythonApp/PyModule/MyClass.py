class MyClass:
   #定义私有属性,私有属性在类外部无法直接进行访问
   __weight = 1
   i=12345

   def __init__(self):
       self.data=[1,2,3]
   
   def f(self):
        return 'hello world'

class MyClass2:
    def __init__(self,realpart,imagepart):
        self.r=realpart
        self.i=imagepart
        print(self)
        print(self.__class__)

    def s(self):
        return 10*10

'''
# 实例化类
x=MyClass()
# 访问类的属性和方法
print("MyClass 类的属性 i 为：", x.i)
print("MyClass 类的方法 f 输出为：", x.f())
'''

#类和类的继承

#类定义
class people:
    #定义基本属性
    name=''
    age=0
    #定义私有属性,私有属性在类外部无法直接进行访问
    __weight=0
    
    #定义构造方法
    def __init__(self,n,a,w):
        self.name=n
        self.age=a
        self.__weight=w

    def speak(self):
        print('%s 说：我%d岁。'%(self.name,self.age))

#单继承示例
class student(people):
    grade=''
    def __init__(self,n,a,w,g):
        #调用父类的构函
        people.__init__(self,n,a,w)
        self.grade=g

     #覆写父类的方法
    def speak(self):
        print('%s说：我%d岁了，我在读%d年级'%(self.name,self.age,self.grade))

#另一个类，多重继承之前的准备
class speaker():
    topic=''
    name=''
    def __init__(self,n,t):
        self.name=n
        self.topic=t
    
    def speak(self):
        print('我叫%s，我是一个演说家，我演讲的主题是%s'%(self.name,self.topic))

#多重继承
class sample(speaker,student):
    a=''
    def __init__(self,n,a,w,g,t):
        student.__init__(self,n,a,w,g)
        speaker.__init__(self,n,t)

    def show(self):
        return student.speak(self)

#调用
test=sample('Tim',25,80,4,'Python')
test.speak() #方法名同，默认调用的是在括号中排前地父类的方法
test.show()

print('-------------------------------------------------')

#方法重写
class parent:
    def myMethod(self):
        print("调用父类方法")

class child(parent): # 定义子类
    def myMethod(self):
        #return super().myMethod()
        print('调用子类方法')

c=child()
c.myMethod() # 子类调用重写方法
super(child,c).myMethod()  #用子类对象调用父类已被覆盖的方法

print('-------------------------------------------------')

#运算符重载
class Vector:
    def __init__(self,a,b):
        self.a=a
        self.b=b

    def __str__(self):
        return 'Vector(%d,%d)'%(self.a,self.b)

    def __add__(self,other):
        return Vector(self.a+other.a,self.b+other.b)

v1 = Vector(2,10)
v2= Vector(5,-2)

print(v1+v2)