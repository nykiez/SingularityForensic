import sqlite3, re,os
import struct

def decrypt_qq(decrypt_qq_imei, encrypt_str):
    imei_len = len(decrypt_qq_imei)
    decrypt_str = ''
    if encrypt_str == None:
        return (decrypt_str)
    else:
        encrypt_str_len = len(encrypt_str)
        for decrypt_qq_i in range(0, encrypt_str_len):
            decrypt_str += chr(ord(encrypt_str[decrypt_qq_i]) ^ ord(decrypt_qq_imei[decrypt_qq_i % imei_len]))
        return (decrypt_str)

def decrypt_qq_msgdata(decrypt_qq_imei, encrypt_str):
    imei_len = len(decrypt_qq_imei)
    decrypt_str = b''
#    print(encrypt_str)
    if encrypt_str == None:
        return (decrypt_str)
    else:
        encrypt_str_len = len(encrypt_str)
        for decrypt_qq_i in range(0, encrypt_str_len):
#            print('test1')
#            print(chr(encrypt_str[decrypt_qq_i] ^ ord(decrypt_qq_imei[decrypt_qq_i % imei_len])))
#            print(struct.pack('B',encrypt_str[decrypt_qq_i]^ord(decrypt_qq_imei[decrypt_qq_i%15])))
            decrypt_str += struct.pack('B',encrypt_str[decrypt_qq_i]^ord(decrypt_qq_imei[decrypt_qq_i % imei_len]))
        try:
#            print('test')
            return (decrypt_str.decode("utf8"))
        except:
            return (str(decrypt_str))










def extract_qq_personal_msgdata(dbname,sqlite_number_name,imei,output_db):  # 提取个人qq聊天记录
    conn_new = sqlite3.connect(output_db)
    cursor_new = conn_new.cursor()
    conn_old = sqlite3.connect(dbname)
    cursor_old = conn_old.cursor()

    try:
        cursor_new.execute('create table WA_MFORENSICS_020500(collect_target_id INTEGER PRIMARY KEY ,'##1此表示先标记无提取
                           'contact_account_type TEXT,'##2数字为在国标中对应内容的序号
                           'account_id TEXT,'##3
                           'account TEXT,'
                           'regis_nickname TEXT,'##5
                           'friend_id TEXT,'##6
                           'friend_account TEXT,'
                           'friend_nickname TEXT,'
                           'content BLOB ,'
                           'mail_send_time INTEGER DEFAULT \'0\','
                           'local_action INTEGER ,'
                           'talk_id TEXT,'##12
                           'delete_status INTEGER DEFAULT \'0\','
                           'delete_time INTEGER)')##14
        conn_new.commit()
    except :
        pass
    try:
        cursor_old.execute('select name from sqlite_master')
        table_values = cursor_old.fetchall()
        for each_table in table_values:
            if re.compile('mr_friend_[0-9A-Z]{32}_New\\Z').match(each_table[0]):
                cursor_old.execute('select frienduin,msgdata,time,issend from %s'%each_table)
                values = cursor_old.fetchall()
                for each_value in values:
                    each_value = list(each_value)
                    each_value[0] = decrypt_qq(imei, each_value[0])
                    each_value[1] = decrypt_qq_msgdata(imei, each_value[1])
                    each_value[3] += 1
                    each_value.insert(0,sqlite_number_name)
                    each_value = tuple(each_value)
                    cursor_new.execute('insert into WA_MFORENSICS_020500(account,friend_account,content,mail_send_time,local_action)VALUES(?,?,?,?,?)',
                                       each_value)
        conn_new.commit()
    except:
        pass
    try:
        cursor_old.execute('select uin,name from friends ')
        name_values = cursor_old.fetchall()
        for each_name in name_values:
                each_name = list(each_name)
                each_name[0] = decrypt_qq(imei, each_name[0])
                each_name[1] = decrypt_qq(imei, each_name[1])
                each_name = tuple(each_name)
                cursor_new.execute('update WA_MFORENSICS_020500 set friend_nickname = ? where friend_account == ?',(each_name[1],each_name[0]))
        conn_new.commit()
    except:
        pass
    cursor_old.close()
    conn_old.close()
    cursor_new.close()
    conn_new.close()

def extract_qq_personal_friends(dbname,sqlite_number_name,imei,output_db):  # 提取个人qq好友
    conn_new = sqlite3.connect(output_db)
    cursor_new = conn_new.cursor()
    conn_old = sqlite3.connect(dbname)
    cursor_old = conn_old.cursor()

    try:
        cursor_new.execute('create table WA_MFORENSICS_020200(collect_target_id INTEGER PRIMARY KEY ,'##1此表示先标记无提取
                           'contact_account_type TEXT,'##2数字为在国标中对应内容的序号
                           'account_id TEXT,'##3
                           'account TEXT,'
                           'friend_id TEXT,'##5
                           'friend_account TEXT,'
                           'friend_nickname TEXT ,'
                           'friend_group INTEGER ,'
                           'friend_remark TEXT ,'
                           'area TEXT ,'##10
                           'city_code TEXT ,'##11
                           'fixed_phone TEXT ,'##12
                           'msisdn TEXT ,'##13
                           'email_account TEXT ,'##14
                           'certificate_type TEXT ,'##15
                           'certificate_code TEXT ,'##16
                           'sexcode INTEGER ,'
                           'age INTEGER ,'
                           'postal_address TEXT ,'##19
                           'postal_code TEXT ,'##20
                           'occupation_name TEXT ,'##21
                           'blood_type TEXT ,'##22
                           'name TEXT ,'##23
                           'sign_name TEXT ,'##24
                           'personal_desc TEXT ,'##25
                           'reg_city TEXT ,'##26
                           'graduateschool TEXT ,'##27
                           'zodiac TEXT ,'##28
                           'constallation TEXT ,'##29
                           'birthday TEXT ,'##30
                           'delete_status INTEGER DEFAULT \'0\','##31
                           'delete_time INTEGER ,'##32
                           'last_login_time INTEGER )')  #猜测此时间戳是好友最后一次登录时间
        conn_new.commit()
    except :
        pass
    try:
        cursor_old.execute('select uin,name,groupid,remark,gender,age,datetime from Friends')
        table_values = cursor_old.fetchall()
        for each_value in table_values:
            each_value = list(each_value)
            each_value[0] = decrypt_qq(imei, each_value[0])
            each_value[1] = decrypt_qq(imei, each_value[1])
            each_value[3] = decrypt_qq(imei, each_value[3])
            each_value.insert(0, sqlite_number_name)
            each_value = tuple(each_value)
            cursor_new.execute('insert into WA_MFORENSICS_020200(account,friend_account,friend_nickname,friend_group,friend_remark,sexcode,age,last_login_time)VALUES(?,?,?,?,?,?,?,?)',each_value)
        conn_new.commit()
    except:
        pass
    cursor_old.close()
    conn_old.close()
    cursor_new.close()
    conn_new.close()















def extract_qq_troop_msgdata(dbname,sqlite_number_name,imei,output_db):  #提取群组聊天记录
    conn_new = sqlite3.connect(output_db)
    cursor_new = conn_new.cursor()
    conn_old = sqlite3.connect(dbname)
    cursor_old = conn_old.cursor()

    try:
        cursor_new.execute('create table WA_MFORENSICS_020600(collect_target_id INTEGER PRIMARY KEY ,'##1此表示先标记无提取
                           'contact_account_type TEXT,'##2数字为在国标中对应内容的序号
                           'account_id TEXT,'##3
                           'account TEXT,'
                           'group_num TEXT,'
                           'group_name TEXT ,'
                           'friend_id TEXT,'##7
                           'friend_account TEXT,'
                           'friend_nickname TEXT,'
                           'content BLOB ,'
                           'mail_send_time INTEGER DEFAULT \'0\','
                           'local_action INTEGER ,'
                           'talk_id TEXT ,'##13
                           'delete_status INTEGER DEFAULT \'0\','##14
                           'delete_time INTEGER,'##15
                           'troop_type TEXT)')
        conn_new.commit()
    except :
        pass
    cursor_old.execute('select name from sqlite_master')
    table_values = cursor_old.fetchall()
    try:
        for each_table in table_values:
            if re.compile('mr_troop_[0-9A-Z]{32}_New\\Z').match(each_table[0]):
                cursor_old.execute('select frienduin,senderuin,msgdata,time,issend from %s' %each_table)
                values = cursor_old.fetchall()
                for each_value in values:
                    each_value = list(each_value)
                    each_value[0] = decrypt_qq(imei, each_value[0])
                    each_value[1] = decrypt_qq(imei, each_value[1])
                    each_value[2] = decrypt_qq_msgdata(imei, each_value[2])
                    each_value[4] += 1
                    each_value.insert(0,sqlite_number_name)
                    each_value.append('group')
                    each_value = tuple(each_value)
                    cursor_new.execute('insert into WA_MFORENSICS_020600(account,group_num,friend_account,content,mail_send_time,local_action,troop_type)VALUES(?,?,?,?,?,?,?)',
                                       each_value)
        conn_new.commit()
    except:
        pass
    try:
        cursor_old.execute('select troopuin,troopname from troopinfo ')
        troop_name_values = cursor_old.fetchall()
        for each_troop_name in troop_name_values:
            each_troop_name = list(each_troop_name)
            each_troop_name[0] = decrypt_qq(imei, each_troop_name[0])
            each_troop_name[1] = decrypt_qq(imei, each_troop_name[1])
            each_troop_name = tuple(each_troop_name)
            cursor_new.execute('update WA_MFORENSICS_020600 set group_name = ? where group_num == ?',
                               (each_troop_name[1], each_troop_name[0]))
        conn_new.commit()
    except:
        pass
    try:
        cursor_old.execute('select memberuin,friendnick from troopmemberinfo ')
        troop_menber_name_values = cursor_old.fetchall()
        for each_troop_menber_name in troop_menber_name_values:
            each_troop_menber_name = list(each_troop_menber_name )
            each_troop_menber_name[0] = decrypt_qq(imei, each_troop_menber_name [0])
            each_troop_menber_name[1] = decrypt_qq(imei, each_troop_menber_name [1])
            each_troop_menber_name = tuple(each_troop_menber_name )
            cursor_new.execute('update WA_MFORENSICS_020600 set friend_nickname = ? where friend_account == ?',
                               (each_troop_menber_name [1], each_troop_menber_name [0]))
        conn_new.commit()
    except:
        pass
    try:
        cursor_old.execute('select name from sqlite_master')
        table_values = cursor_old.fetchall()
        for each_table in table_values:
            if re.compile('mr_discusssion_[0-9A-Z]{32}_New\\Z').match(each_table[0]):
                cursor_old.execute('select frienduin,senderuin,msgdata,time,issend from %s' %each_table)
                values = cursor_old.fetchall()
                for each_value in values:
                    each_value = list(each_value)
                    each_value[0] = decrypt_qq(imei, each_value[0])
                    each_value[1] = decrypt_qq(imei, each_value[1])
                    each_value[2] = decrypt_qq_msgdata(imei, each_value[2])
                    each_value[4] += 1
                    each_value.insert(0,sqlite_number_name)
                    each_value.append('discussion')
                    each_value = tuple(each_value)
                    cursor_new.execute('insert into WA_MFORENSICS_020600(account,group_num,friend_account,content,mail_send_time,local_action,troop_type)VALUES(?,?,?,?,?,?,?)',
                                       each_value)
        conn_new.commit()
    except:
        pass
    try:
        cursor_old.execute('select uin,discussionname from discussioninfo')
        discussion_name_values = cursor_old.fetchall()
        for each_discussion_name in discussion_name_values:
            each_discussion_name = list(each_discussion_name)
            each_discussion_name[0] = decrypt_qq(imei, each_discussion_name[0])
            each_discussion_name[1] = decrypt_qq(imei, each_discussion_name[1])
            each_discussion_name = tuple(each_discussion_name)
            cursor_new.execute('update WA_MFORENSICS_020600 set group_name = ? where group_num == ?',
                               (each_discussion_name[1], each_discussion_name[0]))
        conn_new.commit()
    except:
        pass
    try:
        cursor_old.execute('select memberuin,membername from discussionmemberinfo')
        discussion_menber_name_values = cursor_old.fetchall()
        for each_discussion_menber_name in discussion_menber_name_values:
            each_discussion_menber_name = list(each_discussion_menber_name)
            each_discussion_menber_name[0] = decrypt_qq(imei, each_discussion_menber_name[0])
            each_discussion_menber_name[1] = decrypt_qq(imei, each_discussion_menber_name[1])
            each_discussion_menber_name = tuple(each_discussion_menber_name)
            cursor_new.execute('update WA_MFORENSICS_020600 set friend_nickname = ? where friend_account == ?',
                               (each_discussion_menber_name[1], each_discussion_menber_name[0]))
        conn_new.commit()
    except:
        pass
    cursor_old.close()
    conn_old.close()
    cursor_new.close()
    conn_new.close()





def extract_qq_troop_members(dbname,sqlite_number_name,imei,output_db):  # 提取群组成员
    conn_new = sqlite3.connect(output_db)
    cursor_new = conn_new.cursor()
    conn_old = sqlite3.connect(dbname)
    cursor_old = conn_old.cursor()

    try:
        cursor_new.execute('create table WA_MFORENSICS_020400(collect_target_id INTEGER PRIMARY KEY ,'##1此表示先标记无提取
                           'contact_account_type TEXT,'##2数字为在国标中对应内容的序号
                           'account_id TEXT,'##3'
                           'account TEXT,'
                           'group_num TEXT ,'
                           'group_name TEXT ,'
                           'friend_id TEXT,'#7
                           'friend_account TEXT ,'
                           'friend_nickname TEXT ,'
                           'friend_remark TEXT,'
                           'area TEXT ,'##11
                           'city_code TEXT ,'##12
                           'fixed_phone TEXT ,'##13
                           'msisdn TEXT ,'##14
                           'email_account TEXT ,'##15
                           'certificate_type TEXT ,'##16
                           'certificate_code TEXT ,'##17
                           'sexcode INTEGER ,'
                           'age INTEGER ,'
                           'postal_address TEXT ,'##20
                           'postal_code TEXT ,'##21
                           'occupation_name TEXT ,'##22
                           'blood_type TEXT ,'##23
                           'name TEXT ,'##24
                           'sign_name TEXT ,'##25
                           'personal_desc TEXT ,'##26
                           'reg_city TEXT ,'##27
                           'graduateschool TEXT ,'##28
                           'zodiac TEXT ,'##29
                           'constallation TEXT ,'##30
                           'birthday TEXT ,'##31
                           'delete_status INTEGER DEFAULT \'0\','##32
                           'delete_time INTEGER ,'##33
                           'last_msg_inform INTEGER ,'#此为推测未经测试
                           'troop_type TEXT )')
        conn_new.commit()
    except :
        pass
    try:
        cursor_old.execute('select troopuin,memberuin,friendnick,troopnick,sex,age,datetime from troopmemberinfo')
        table_values = cursor_old.fetchall()
        for each_value in table_values:
            each_value = list(each_value)
            each_value[0] = decrypt_qq(imei, each_value[0])
            each_value[1] = decrypt_qq(imei, each_value[1])
            each_value[2] = decrypt_qq(imei, each_value[2])
            each_value[3] = decrypt_qq(imei, each_value[3])
            each_value.insert(0, sqlite_number_name)
            each_value.append('group')
            each_value = tuple(each_value)
            cursor_new.execute('insert into WA_MFORENSICS_020400(account,group_num,friend_account,friend_nickname,friend_remark,sexcode,age,last_msg_inform,troop_type)VALUES(?,?,?,?,?,?,?,?,?)',each_value)
        conn_new.commit()
    except:
        pass
    try:
        cursor_old.execute('select troopuin,troopname from troopinfo ')
        troop_name_values = cursor_old.fetchall()
        for each_troop_name in troop_name_values:
            each_troop_name = list(each_troop_name)
            each_troop_name[0] = decrypt_qq(imei, each_troop_name[0])
            each_troop_name[1] = decrypt_qq(imei, each_troop_name[1])
            each_troop_name = tuple(each_troop_name)
            cursor_new.execute('update WA_MFORENSICS_020400 set group_name = ? where group_num == ?',
                               (each_troop_name[1],each_troop_name[0]))
        conn_new.commit()
    except:
        pass
    try:
        cursor_old.execute('select discussionuin,memberuin,membername,interemark,datatime from discussionmemberinfo')
        table_values = cursor_old.fetchall()
        for each_value in table_values:
            each_value = list(each_value)
            each_value[0] = decrypt_qq(imei, each_value[0])
            each_value[1] = decrypt_qq(imei, each_value[1])
            each_value[2] = decrypt_qq(imei, each_value[2])
            each_value[3] = decrypt_qq(imei, each_value[3])
            each_value.insert(0, sqlite_number_name)
            each_value.append('discussion')
            each_value = tuple(each_value)
            cursor_new.execute('insert into WA_MFORENSICS_020400(account,group_num,friend_account,friend_nickname,friend_remark,last_msg_inform,troop_type)VALUES(?,?,?,?,?,?,?)',each_value)
        conn_new.commit()
    except:
        pass
    try:
        cursor_old.execute('select uin,discussionname from discussioninfo ')
        troop_name_values = cursor_old.fetchall()
        for each_troop_name in troop_name_values:
            each_troop_name = list(each_troop_name)
            each_troop_name[0] = decrypt_qq(imei, each_troop_name[0])
            each_troop_name[1] = decrypt_qq(imei, each_troop_name[1])
            each_troop_name = tuple(each_troop_name)
            cursor_new.execute('update WA_MFORENSICS_020400 set friend_name = ? where friend_num == ?',
                               (each_troop_name[1],each_troop_name[0]))
        conn_new.commit()
    except:
        pass
    cursor_old.close()
    conn_old.close()
    cursor_new.close()
    conn_new.close()


#imei= '867064029171533'
#    '269748759.db'
#extract_qq_personal_friends("/Volumes/DATA/镜像/output/23/data/com.tencent.mobileqq/databases/53077093.db")