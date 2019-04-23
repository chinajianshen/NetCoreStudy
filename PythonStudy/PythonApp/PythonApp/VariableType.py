"""
type()不会认为子类是一种父类类型。
isinstance()会认为子类是一种父类类型。
"""

tup1 = ()
print("tup1 = ():",tup1)
tup2=(20)
print("tup2=(20):",tup2)
tup3=(20,)
print("tup3=(20,):",tup3)

a,b,c,d=20,5.5,True,4+3j
print(type(a), type(b), type(c), type(d))
a=111
print("isinstance(a,int)):",isinstance(a,int)) #True

print('----------------------------------------------')
class A:
    pass

class B(A):
    pass

print("isinstance(A(),A):",isinstance(A(),A))
print("type(A()) == A:",type(A()) == A)
print("isinstance(B(),A):",isinstance(B(),A))
print("type(B()) == A:",type(B()) == A)

print('----------------------------------------------')
s='abcdef'
print(s[1:5]) #bcdef
print(s[:5]) #abcdef
print(s[:])#abcdef
print(s[-3:-1]) #de
print(s[-3:]) #def


str='Hello World!'
print(str)           # 输出完整字符串
print( str[0])        # 输出字符串中的第一个字符
print (str[2:5])     # 输出字符串中第三个至第六个之间的字符串
print (str[2:])    # 输出从第三个字符开始的字符串
print (str * 2)       # 输出字符串两次
print (str + "TEST")  # 输出连接的字符串

print('---------------字符串-------------------')
#列表
letters = ['a','b','c','d','e','f','g']
print(letters[1:4:2]) # b d 参数作用是截取的步长，以下实例在索引 1 到索引 4 的位置并设置为步长为 2（间隔一个位置）来截取字符串：
print(letters[0::2]) #a c e g 

list = ['runoob',786,2.23,'john',70.2]
tinylist =[123,'john']
tinylist[1]='222'

print (list)               # 输出完整列表 ['runoob', 786, 2.23, 'john', 70.2]
print (list[0])            # 输出列表的第一个元素 runoob
print (list[1:3])          # 输出第二个至第三个元素 [786, 2.23]
print (list[2:])           # 输出从第三个开始至列表末尾的所有元素[2.23, 'john', 70.2]
print (tinylist * 2)      # 输出列表两次[123, 'john', 123, 'john']
print (list + tinylist)    # 打印组合的列表['runoob', 786, 2.23, 'john', 70.2, 123, 'john']
print("----------------列表------------------")

#元组 元组用 () 标识。内部元素用逗号隔开。但是元组不能二次赋值，相当于只读列表。
tuple=('runoob',786,2.23,'john',70.2)
tinytuple=(123,'john')

print(tuple) #('runoob', 786, 2.23, 'john', 70.2)
print(tuple[0]) #runoob
print(tuple[1:3])#(786, 2.23)
print(tuple[1:4]) #(786, 2.23,'john')
print(tuple[2:])#(2.23, 'john', 70.2)
print(tinytuple*2)#(123, 'john', 123, 'john')
print(tuple+tinytuple)#('runoob', 786, 2.23, 'john', 70.2, 123, 'john')


#元组无效的，因为元组是不允许更新的。而列表是允许更新的：

#tuple = ( 'runoob', 786 , 2.23, 'john', 70.2 )
#list = [ 'runoob', 786 , 2.23, 'john', 70.2 ]
#tuple[2] = 1000    # 元组中是非法应用
#list[2] = 1000     # 列表中是合法应用

print("----------------元组------------------")

dict={}
dict['one']="This is one"
dict[2]="This is two"

print(dict) #{'one': 'This is one', 2: 'This is two'}
print(dict['one'])
print(dict[2])

tinydict = {'name': 'john','code':6734, 'dept': 'sales'}

print(tinydict)
print(tinydict.keys())#dict_keys(['name', 'code', 'dept'])
print(tinydict.values())

#dict1 = dict([('Runoob', 1), ('Google', 2), ('Taobao', 3)])
#print("dict1=",dict1)
print("----------------字典------------------")

int1 = int('123')
print(int1)
var1 = '123'
print(var1)

eval1 = eval('1+2')
print(eval1)

print("----------------------------------")

#可以使用大括号 { } 或者 set() 函数创建集合，注意：创建一个空集合必须用 set() 而不是 { }，因为 { } 是用来创建一个空字典。

dict1 = {0:"Tom",1:"Jim"} #字典
student={'Tom', 'Jim', 'Mary', 'Tom', 'Jack', 'Rose'} #集合  
print(student)

a = set('abracadabra')
b = set('alacazam')
print(a,end="  ")
print(b)
print("a-b=",a-b)# a 和 b 的差集 {'d', 'b', 'r'}
print(a | b)     # a 和 b 的并集 
print(a & b)     # a 和 b 的交集 
print(a ^ b)     # a 和 b 中不同时存在的元素


