import support
import fibo

#from . import echo
#from .. import formats
#from ..filters import equalizer

#import PyModule.using_sys #第一种导入方法
#PyModule.using_sys.test1() #第一种方法调用

from PyModule import using_sys #第二种导入方法
using_sys.test1() #第二种方法调用

#from fibo import fib, fib2 #这个声明不会把整个fibo模块导入到当前的命名空间中，它只会将fibo里的fib函数引入进来
#from fibo import * #这将把所有的名字都导入进来，但是那些由单一下划线（_）开头的名字不在此例。大多数情况， Python程序员不使用这种方法，因为引入的其它来源的命名，很可能覆盖了已有的定义。

#fib00 = fibo.fib #声明一个方法
#fib22 = fibo.fib2
#fib33=fibo.fib2(30)
support.print_func('Runoob')
#support.print_syspath()
#fibo.fib(10)
#fib00(10)
#print(fib22(20))
#print(fib33)

#print(dir(fibo))
#print(dir()) #内置的函数 dir() 可以找到模块内定义的所有名称。以一个字符串列表的形式返回:
