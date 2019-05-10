import mysql.connector
mydb=mysql.connector.connect(
    host="localhost",
    user="root",
    passwd="sa."
)

print(mydb)

mycursor = mydb.cursor()
#mycursor.execute("CREATE DATABASE runoob_db")

mycursor.execute("SHOW DATABASES")
for x in mycursor:
    print(x)

mydb.close()

mydb=mysql.connector.connect(
   host='localhost',
   user='root',
   passwd='sa.',
   database='runoob_db'
)

mycursor=mydb.cursor()
#mycursor.execute('drop table sites')
#mycursor.execute("CREATE TABLE sites (id INT AUTO_INCREMENT PRIMARY KEY, name VARCHAR(255), url VARCHAR(255))")
mycursor.execute("SHOW TABLES")
for x in mycursor:
    print(x)

'''
#单条插入
sql='INSERT INTO sites (name, url) VALUES (%s, %s)'
val=("RUNOOB", "https://www.runoob.com")
mycursor.execute(sql,val)
mydb.commit() # 数据表内容有更新，必须使用到该语句
mycursor.lastrowid #在数据记录插入后，获取该记录的 ID
print(mycursor.rowcount, "记录插入成功。")
'''

#批量插入
'''
sql = "INSERT INTO sites (name, url) VALUES (%s, %s)"
val = [
  ('Google', 'https://www.google.com'),
  ('Github', 'https://www.github.com'),
  ('Taobao', 'https://www.taobao.com'),
  ('stackoverflow', 'https://www.stackoverflow.com/')
]
 
mycursor.executemany(sql,val)
mydb.commit()
print(mycursor.rowcount, "记录插入成功。")
'''
print('==============================================')
#查询
mycursor.execute("select * from sites")
#myone =mycursor.fetchone() #取一条数据
#print(myone)
print('==============================================')
myresult=mycursor.fetchall() #取本次所有
for x in myresult:
    print(x)
