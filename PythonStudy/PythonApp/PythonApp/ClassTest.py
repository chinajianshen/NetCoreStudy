from PyModule import MyClass


x = MyClass.MyClass()
print('111',x._MyClass__weight)
print('MyClass类属性i:',x.i)
print('MyClass类方法f():',x.f())
print('MyClass类构造函数赋值调用：',x.data)
#print('MyClass类直接调用',MyClass.MyClass.f()) #直接调用类方法异常

print('==========================================')

y=MyClass.MyClass2(10,20)
print('MyClass类构造函数赋值调用：%d,%d' % (y.i,y.r))



#类与继承
s=MyClass.people('人',20,50)
s.speak()

s=MyClass.student('ken',10,60,3)
s.speak()
