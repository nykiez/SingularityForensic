import sqlite3

def extract_wechat_personal_msgdata(dbname,output_dbname):  #提取微信个人聊天信息
    conn_new = sqlite3.connect(output_dbname)
    cursor_new = conn_new.cursor()
    conn_old = sqlite3.connect(dbname)
    cursor_old = conn_old.cursor()
    try:
        cursor_new.execute('create table WA_MFORENSICS_020500(collect_target_id INTEGER PRIMARY KEY ,'##1此表示先标记无提取
                           'contact_account_type TEXT,'##2数字为在国标中对应内容的序号
                           'account_id TEXT,'##3
                           'account TEXT,'##4
                           'regis_nickname TEXT,'##5
                           'friend_id TEXT,'##6
                           'friend_account TEXT,'
                           'friend_nickname TEXT,'
                           'content BLOB ,'
                           'mail_send_time INTEGER DEFAULT \'0\','
                           'local_action INTEGER ,'
                           'talk_id TEXT,'
                           'delete_status INTEGER DEFAULT \'0\','##13
                           'delete_time INTEGER)')##14
        conn_new.commit()
    except :
        pass
    try:
        cursor_old.execute('select talker,content,createtime,issend,msgId from message where talker like \'wxid%\' order by msgid')
        values = cursor_old.fetchall()
        for each_value in values:
            cursor_new.execute('insert into WA_MFORENSICS_020500(friend_account,content,mail_send_time,local_action,talk_id)VALUES(?,?,?,?,?)',each_value)
        conn_new.commit()
    except:
        pass
    try:
        cursor_old.execute('select username,nickname from rcontact')
        values = cursor_old.fetchall()
        for each_value in values:
            cursor_new.execute('update WA_MFORENSICS_020500 set friend_nickname = ? where friend_account == ?',(each_value[1],each_value[0]))
        conn_new.commit()
    except:
        pass
    cursor_old.close()
    conn_old.close()
    cursor_new.close()
    conn_new.close()


def extract_wechat_personal_friends(dbname,output_dbname):  # 提取个人微信好友
    conn_new = sqlite3.connect(output_dbname)
    cursor_new = conn_new.cursor()
    conn_old = sqlite3.connect(dbname)
    cursor_old = conn_old.cursor()

    try:
        cursor_new.execute('create table WA_MFORENSICS_020200(collect_target_id INTEGER PRIMARY KEY ,'##1此表示先标记无提取
                           'contact_account_type TEXT,'##2数字为在国标中对应内容的序号
                           'account_id TEXT,'##3
                           'account TEXT,'##4
                           'friend_id TEXT,'
                           'friend_account TEXT,'
                           'friend_nickname TEXT ,'
                           'friend_group INTEGER ,'##8
                           'friend_remark TEXT ,'
                           'area TEXT ,'##10
                           'city_code TEXT ,'##11
                           'fixed_phone TEXT ,'##12
                           'msisdn TEXT ,'##13
                           'email_account TEXT ,'##14
                           'certificate_type TEXT ,'##15
                           'certificate_code TEXT ,'##16
                           'sexcode INTEGER ,'##17
                           'age INTEGER ,'##18
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
                           'last_login_time INTEGER )') ##33
        conn_new.commit()
    except :
        pass
    try:
        cursor_old.execute('select username,alias,nickname,conRemark from rcontact')
        table_values = cursor_old.fetchall()
        for each_value in table_values:
            cursor_new.execute('insert into WA_MFORENSICS_020200(friend_id,friend_account,friend_nickname,friend_remark)VALUES(?,?,?,?)',each_value)
        conn_new.commit()
    except:
        pass
    cursor_old.close()
    conn_old.close()
    cursor_new.close()
    conn_new.close()

def extract_wechat_troop_members(dbname,output_dbname):  # 提取群组成员
    conn_new = sqlite3.connect(output_dbname)
    cursor_new = conn_new.cursor()
    conn_old = sqlite3.connect(dbname)
    cursor_old = conn_old.cursor()

    try:
        cursor_new.execute('create table WA_MFORENSICS_020400(collect_target_id INTEGER PRIMARY KEY ,'##1此表示先标记无提取
                           'contact_account_type TEXT,'##2数字为在国标中对应内容的序号
                           'account_id TEXT,'##3'
                           'account TEXT,'##4
                           'group_num TEXT ,'
                           'group_name TEXT ,'
                           'friend_id TEXT,'#7
                           'friend_account TEXT ,'
                           'friend_nickname TEXT ,'##9
                           'friend_remark TEXT,'##10
                           'area TEXT ,'##11
                           'city_code TEXT ,'##12
                           'fixed_phone TEXT ,'##13
                           'msisdn TEXT ,'##14
                           'email_account TEXT ,'##15
                           'certificate_type TEXT ,'##16
                           'certificate_code TEXT ,'##17
                           'sexcode INTEGER ,'##18
                           'age INTEGER ,'##19
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
                           'last_msg_inform INTEGER ,'##34
                           'troop_type TEXT )')##35
        conn_new.commit()
    except :
        pass
    try:
        cursor_old.execute('select chatroomname,displayname,memberlist from chatroom ')
        values = cursor_old.fetchall()
        for each_value in values:
            chatroom_member_id = each_value[2].split(';')
            for each_chatroom_member_id in chatroom_member_id:
                each_value = list(each_value)
                each_value[2] = each_chatroom_member_id
                each_value = tuple(each_value)
                cursor_new.execute('insert into WA_MFORENSICS_020400(group_num,group_name,friend_account)VALUES(?,?,?)',each_value)
        conn_new.commit()
    except:
        pass
    cursor_old.close()
    conn_old.close()
    cursor_new.close()
    conn_new.close()



def extract_wechat_troop_msgdata(dbname,output_dbname):  #提取群组聊天记录
    conn_new = sqlite3.connect(output_dbname)
    cursor_new = conn_new.cursor()
    conn_old = sqlite3.connect(dbname)
    cursor_old = conn_old.cursor()

    try:
        cursor_new.execute('create table WA_MFORENSICS_020600(collect_target_id INTEGER PRIMARY KEY ,'##1此表示先标记无提取
                           'contact_account_type TEXT,'##2数字为在国标中对应内容的序号
                           'account_id TEXT,'##3
                           'account TEXT,'##4
                           'group_num TEXT,'
                           'group_name TEXT ,'##6
                           'friend_id TEXT,'##7
                           'friend_account TEXT,'##8
                           'friend_nickname TEXT,'##9
                           'content BLOB ,'
                           'mail_send_time INTEGER DEFAULT \'0\','
                           'local_action INTEGER ,'
                           'talk_id TEXT ,'
                           'delete_status INTEGER DEFAULT \'0\','##14
                           'delete_time INTEGER,'##15
                           'troop_type TEXT)')##16
        conn_new.commit()
    except :
        pass
    try:
        cursor_old.execute('select talker,content,createtime,issend,msgId from message where talker like \'%@chatroom\' order by msgid')
        table_values = cursor_old.fetchall()
        for each_value in table_values:
            cursor_new.execute(
                'insert into WA_MFORENSICS_020600(group_num,content,mail_send_time,local_action,talk_id)VALUES(?,?,?,?,?)',
                each_value)
        conn_new.commit()
    except:
        pass
    cursor_old.close()
    conn_old.close()
    cursor_new.close()
    conn_new.close()
    
def get_wxid(dbname):
    conn_old = sqlite3.connect(dbname)
    cursor_old = conn_old.cursor()
    try:
        cursor_old.execute('select value from userinfo where id = 2')
        table_values = cursor_old.fetchall()[0][0]
        cursor_old.close()
        conn_old.close()
        return(table_values)
    except:
        pass
    
#extract_wechat_personal_msgdata('decrypto.db','output.db')
#extract_wechat_personal_friends('decrypto.db','output.db')
#extract_wechat_troop_members('decrypto.db','output.db')
#extract_wechat_troop_msgdata('decrypto.db','output.db')
#print(get_wxid('/Volumes/DATA/镜像/output/mms/23/data/com.tencent.mm/MicroMsg/818ec766fbdbfffcf788bfa0f069812a/EnMicroMsg_decrypt.db'))