import time


ticks=time.time() #获取当前时间戳 每个时间戳都以自从1970年1月1日午夜（历元）经过了多长时间来表示
print('当前时间戳为:',ticks)


#获取当前时间
localtime = time.localtime(time.time())
print ("本地时间为 :", localtime) #time.struct_time(tm_year=2016, tm_mon=4, tm_mday=7, tm_hour=10, tm_min=28, tm_sec=49, tm_wday=3, tm_yday=98, tm_isdst=0)


#获取格式化的时间 
#最简单的获取可读的时间模式的函数是asctime()

localtime  = time.asctime(time.localtime(time.time()))
print ("本地时间为 :", localtime)

print('----------------------------------------------------------')
#格式化日期
## 格式化成2016-03-20 11:45:39形式
print(time.strftime('%Y-%m-%d %H:%M:%S',time.localtime()))
print(time.strftime('%Y/%m/%d %H:%M:%S',time.localtime()))
#print(time.strftime('%y-%m-%d %h:%m:%s',time.localtime())) #无效格式 主要大小写

# 格式化成Sat Mar 28 22:24:24 2016形式
print(time.strftime('%a %b %d %H:%M:%S %Y',time.localtime()))

# 将格式字符串转换为时间戳
a = "Sat Mar 28 22:24:24 2019"
print(time.mktime(time.strptime(a,'%a %b %d %H:%M:%S %Y')))

print(time.strftime('%c',time.localtime()))
print(time.strftime('%j',time.localtime()))


print('----------------------------------------------------------')
import calendar
cal =calendar.month(2019,4)
print ("以下输出2016年1月份的日历:")
print (cal)