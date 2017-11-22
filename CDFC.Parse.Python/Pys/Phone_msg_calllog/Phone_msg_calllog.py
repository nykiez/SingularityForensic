import sqlite3,os
def extract_phone_call_log(dbname, output_dbname): #提取用户手机通话记录
    conn_new = sqlite3.connect(output_dbname)
    cursor_new = conn_new.cursor()
    conn_old = sqlite3.connect(dbname)
    cursor_old = conn_old.cursor()
    try:
        cursor_new.execute('create table WA_MFORENSICS_010600(collect_target_id INTEGER PRIMARY KEY ,'##
                           'msisdn TEXT ,'##
                           'relationship_account TEXT ,'
                           'relationship_name TEXT ,'
                           'call_status INTEGER ,'##
                           'local_action INTEGER ,'
                           'start_time INTEGER ,'
                           'end_time INTEGER ,'##
                           'dual_time INTEGER ,'
                           'privacyconfig INTEGER DEFAULT \'0\' ,'##
                           'delete_status INTEGER ,'##
                           'delete_time INTEGER )')##
        conn_new.commit()
    except :
        pass
    try:
        cursor_old.execute('select number,name,type,date,duration from calls')
        values = cursor_old.fetchall()
        for each_value in values:
            cursor_new.execute('insert into WA_MFORENSICS_010600(relationship_account,relationship_name,local_action,start_time,dual_time)VALUES(?,?,?,?,?)',
                each_value)
        conn_new.commit()
    except :
        pass
    cursor_old.close()
    conn_old.close()
    cursor_new.close()
    conn_new.close()


def extract_phone_sms(dbname, output_dbname): #提取用户手机短信记录
    conn_new = sqlite3.connect(output_dbname)
    cursor_new = conn_new.cursor()
    conn_old = sqlite3.connect(dbname)
    cursor_old = conn_old.cursor()
    try:
        cursor_new.execute('create table WA_MFORENSICS_010700(collect_target_id INTEGER PRIMARY KEY ,'##
                           'msisdn TEXT ,'##
                           'relationship_account TEXT ,'
                           'relationship_name TEXT ,'##
                           'local_action INTEGER ,'
                           'mail_send_time INTEGER ,'
                           'content TEXT,'
                           'mail_view_status TEXT ,'
                           'mail_save_folder TEXT,'##
                           'privacyconfig INTEGER DEFAULT \'0\','##
                           'delete_status INTEGER ,'
                           'delete_time INTEGER )')##
        conn_new.commit()
    except :
        pass
    try:
        cursor_old.execute('select address,type,date,body,read from sms')
        values = cursor_old.fetchall()
        for each_value in values:
            cursor_new.execute('insert into WA_MFORENSICS_010700(relationship_account,local_action,mail_send_time,content,mail_view_status) VALUES(?,?,?,?,?)',each_value)
        conn_new.commit()
    except :
        pass
    cursor_old.close()
    conn_old.close()
    cursor_new.close()
    conn_new.close()



def extract_raw_contact(dbname, output_dbname): #提取用户手机通讯录
    conn_new = sqlite3.connect(output_dbname)
    cursor_new = conn_new.cursor()
    conn_old = sqlite3.connect(dbname)
    cursor_old = conn_old.cursor()
    try:
        cursor_new.execute('create table WA_MFORENSICS_010400(collect_target_id INTEGER PRIMARY KEY ,'##
                           'sequence_name TEXT ,'
                           'relationship_name TEXT ,'
                           'privacyconfig INTEGER DEFAULT \'0\','##是否加密
                           'delete_status INTEGER ,'
                           'delete_time INTEGER)')##
        conn_new.commit()
    except :
        pass
    try:
        cursor_old.execute('select _id,display_name,deleted from raw_contacts')
        values = cursor_old.fetchall()
        for each_value in values:
            cursor_new.execute('insert into WA_MFORENSICS_010400(sequence_name,relationship_name,delete_status)VALUES(?,?,?)',each_value)
        conn_new.commit()
    except :
        pass
    cursor_old.close()
    conn_old.close()
    cursor_new.close()
    conn_new.close()




def extract_contact_data(dbname, output_dbname):  # 提取用户手机联系人信息记录
    conn_new = sqlite3.connect(output_dbname)
    cursor_new = conn_new.cursor()
    conn_old = sqlite3.connect(dbname)
    cursor_old = conn_old.cursor()
    try:
        cursor_new.execute('create table WA_MFORENSICS_010500(collect_target_id INTEGER PRIMARY KEY ,'##
                           'sequence_name TEXT ,'
                           'phone_value_type INTEGER ,'
                           'phone_number_type TEXT ,'##
                           'relationship_account TEXT ,'
                           'delete_status INTEGER ,' ##
                           'delete_time INTEGER )')##
        conn_new.commit()
    except:
        pass
    try:
        cursor_old.execute('select raw_contact_id,data1 from data where mimetype_id == 5 ORDER BY raw_contact_id')
        values = cursor_old.fetchall()
        for each_value in values:
            each_value = list(each_value)
            each_value.insert(1,'01')
            each_value = tuple(each_value)
            cursor_new.execute(
                'insert into WA_MFORENSICS_010500(sequence_name,phone_value_type,relationship_account)VALUES(?,?,?)',
                each_value)
        conn_new.commit()
    except:
        pass
    cursor_old.close()
    conn_old.close()
    cursor_new.close()
    conn_new.close()


#def main():
#    os.chdir('E:\专业\python\python\Python35\\1')       #/data/data/com.Android.providers.contacts/databases/contacts2.db
#    extract_phone_call_log('contacts2.db','output.db')
#    extract_raw_contact('contacts2.db','output.db')
#    extract_contact_data('contacts2.db','output.db')
#    extract_phone_sms('mmssms.db','output.db')
#
#extract_phone_sms('/Users/july/Documents/mmssms.db','output.db')
#main()