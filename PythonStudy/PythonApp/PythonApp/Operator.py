a=21
b=10
c=0

print(a/b) #2.1
print(a%b) #1
print(2**5)#幂运算 2的5次方 32
print(9//2) #取整 4
print(-9//2) #-5


print('----------------位运算----------------')
a1 = 60 # 0011 1100 
b1 = 13 # 0000 1101  

print(a1&b1) #0000 1100 12

print('----------------逻辑运算----------------')
a = 10
b = 20
 
 
if  a and b :
   print( "1 - 变量 a 和 b 都为 true")
else:
   print ("1 - 变量 a 和 b 有一个不为 true")
 
if  a or b :
   print ("2 - 变量 a 和 b 都为 true，或其中一个变量为 true")
else:
   print ("2 - 变量 a 和 b 都不为 true")
 
# 修改变量 a 的值
a = 0
if  a and b :
   print ("3 - 变量 a 和 b 都为 true")
else:
   print ("3 - 变量 a 和 b 有一个不为 true")
 
if  a or b :
   print ("4 - 变量 a 和 b 都为 true，或其中一个变量为 true")
else:
   print ("4 - 变量 a 和 b 都不为 true")

if not( a and b ):
   print ("5 - 变量 a 和 b 都为 false，或其中一个变量为 false")
else:
   print ("5 - 变量 a 和 b 都为 true")

print('----------------in 和 not in 成员运算符----------------')

a=10
b=20
list=[1,2,3,4,5]

if ( a in list ):
   print( "1 - 变量 a 在给定的列表中 list 中")
else:
   print("1 - 变量 a 不在给定的列表中 list 中")
 
if ( b not in list ):
   print("2 - 变量 b 不在给定的列表中 list 中")
else:
   print ("2 - 变量 b 在给定的列表中 list 中")
 
# 修改变量 a 的值
a = 2
if ( a in list ):
   print ("3 - 变量 a 在给定的列表中 list 中")
else:
   print ("3 - 变量 a 不在给定的列表中 list 中")

print('----------------is 和 is not 身份运算符----------------')

a = 20
b = 20
 
if ( a is b ):
   print ("1 - a 和 b 有相同的标识")
else:
   print( "1 - a 和 b 没有相同的标识")
 
if ( a is not b ):
   print( "2 - a 和 b 没有相同的标识")
else:
   print ("2 - a 和 b 有相同的标识")
 
# 修改变量 b 的值
b = 30
if ( a is b ):
   print ("3 - a 和 b 有相同的标识")
else:
   print ( "3 - a 和 b 没有相同的标识")
 
if ( a is not b ):
   print ("4 - a 和 b 没有相同的标识")
else:
   print ("4 - a 和 b 有相同的标识")

#is 用于判断两个变量引用对象是否为同一个(同一块内存空间)， == 用于判断引用变量的值是否相等。
a=[1,2,3]
b=a
print(b,a)
print("b is a=",b is a) #True
print("b == a=",b == a) #True

print("-----")
b=a[:]
print(b,a)
print("b is a=",b is a)
print("b == a=",b ==a)

print('----------------循环递归----------------')


'''
#不对
BOARD_SIZE = 8

def under_attack(col, queens):
   left = right = col
   for r, c in reversed(queens):
 #左右有冲突的位置的列号
       left, right = left - 1, right + 1

       if c in (left, col, right):
           return True
   return False

def solve(n):
   if n == 0:
       return [[]]

   smaller_solutions = solve(n - 1)

   return [solution+[(n,i+1)]
       for i in xrange(BOARD_SIZE)
           for solution in smaller_solutions
               if not under_attack(i+1, solution)]

for answer in solve(BOARD_SIZE):
   print(answer)
'''