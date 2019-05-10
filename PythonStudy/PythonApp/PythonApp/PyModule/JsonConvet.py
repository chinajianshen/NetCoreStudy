import json

data={
   'no':1,
   'name':'Runoob',
   'url':'http://www.runoob.com'
}

# Python 字典类型转换为 JSON 对象
json_str=json.dumps(data)
print('Python原始数据：',repr(data))
print('JSON对象：',json_str)

#将 JSON 对象转换为 Python 字典
data2=json.loads(json_str)
print('Python对象：',data2)
print ("data2['name']: ", data2['name'])

print('-----------------------------------------')
#要处理的是文件而不是字符串，你可以使用 json.dump() 和 json.load() 来编码和解码JSON数据
import os,sys
path= os.getcwd()

if __name__=="__main__":

    print("__file__=%s" % __file__)

    print("os.path.realpath(__file__)=%s" % os.path.realpath(__file__))

    print ("os.path.dirname(os.path.realpath(__file__))=%s" % os.path.dirname(os.path.realpath(__file__)))
    
    print ("os.path.split(os.path.realpath(__file__))=%s" %( os.path.split(os.path.realpath(__file__))[0]))

    print ("os.path.abspath(__file__)=%s" % os.path.abspath(__file__))

    print ("os.getcwd()=%s" % os.getcwd())

    print ("sys.path[0]=%s" % sys.path[0])

    print ("sys.argv[0]=%s" % sys.argv[0])


print('-----------------------------------------')
filePath=path+r'\data.json'

# 写入 JSON 数据
'''
with open(filePath,'w') as f:
    json.dump(data,f)
'''

# 读取数据
with open(filePath,'r') as f:
    jsonresult =json.load(f)

print(jsonresult)

