import sys

print('命令行参数如下:')
for i in sys.argv:
    print(i)

print('\n\nPython路径为：',sys.path,'\n')

def test1():
    print ("test1()方法")

