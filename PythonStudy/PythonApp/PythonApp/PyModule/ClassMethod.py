#Python中3种方式定义类方法, 常规方式, @classmethod修饰方式, @staticmethod修饰方式.

class A(object):
    def foo(self,x):
        print("executing foo(%s,%s)" %(self,x))
        print("self:",self)

    @classmethod
    def class_foo(cls,x):
        print("executing class_foo(%s,%s)" % (cls, x))
        print('cls:', cls)

    @staticmethod
    def static_foo(x):
         print("executing static_foo(%s)" % x)    
         

a=A()
a.foo(2)
A.foo(a,2) #foo如下方式可以使用正常，显式的传递实例参数a
#A.foo(1)  #报错

a.class_foo(3)
A.class_foo(3)

a.static_foo(4)
A.static_foo(4)



