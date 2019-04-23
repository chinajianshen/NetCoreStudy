
#!/usr/bin/python

count=0
while(count<9):
   print("Index:",count)
   count+=1

print("Goode bye!")

print('------------------------------------')
i=1

'''
var = 1
while var == 1 :  # 该条件永远为true，循环将无限执行下去
   num = input("Enter a number  :")
   print ("You entered: ", num)
   if (num == 5):
       break
'''


count=0
while count<5:
    print(count," is less than 5")
    count=count+1
else:
     print(count," is not less than 5")

print("Goode bye!")

print('----------------while循环--------------------')

for letter in 'python':
   print('当前字母：',letter)
   print("11")
   if (letter == 'o'):
       break

fruits = ['banana','apple','mango']
for fruit in fruits:
    print('当前水果：',fruit)

print('----------')
fruits = ['banana', 'apple',  'mango']
for index in range(len(fruits)):
    print("当前水果：",fruits[index])

print(range(len(fruits))) #(0,3)
print("Good bye")

print('------------')

for num in range(10,20):
    for i in range(2,num):
        if num%i == 0:
            j=num/i
            print('%d 等于 %d * %d' %(num,i,j))
            break
    else:
        print(num,'是一个质数')

print('----------------for循环--------------------')

i=2
while (i<100):
    j=2
    while(j <= i/j):
        if not(i%j):break
        j=j+1
    if (j>i/j):print(i,'是素数')
    i=i+1

print("Good Bye")
print( r"this is a line with \n") #转义不换行
print( "this is a line with \n") #换行

    
    

     
