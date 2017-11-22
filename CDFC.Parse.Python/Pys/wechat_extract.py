import os,re,hashlib,sys,subprocess
import Wechat.wechat_sqlite as wechat_sqlite
import Common.read_imei as read_imei
import Common.Common_ImgRead as Common_ImgRead
import xml.etree.ElementTree as ET

def calculate_password(uin_dir,imei_nums):
    #uin_dir = search_file('E:\\data\\com.tencent.mm\\shared_prefs','auth_info_key_prefs.xml')#找到uin
    f_uin =open(uin_dir)
    for each_line in f_uin:
#        print(each_line)
        if each_line.find("_auth_uin")+1:
#            print(each_line)
            num1 = re.search(r'value="(.*)"', each_line)#正则匹配
            if num1 == None:
                continue
            uin_nums = num1.group(1)
    f_uin.close()
    str = imei_nums + uin_nums
    md5 = hashlib.md5(str.encode('utf-8')).hexdigest()
    return md5[0:7]#计算hash并取其前七位

#each_file_name="/Volumes/DATA/镜像/output/23/data/com.tencent.mm/shared_prefs/auth_info_key_prefs.xml"
#imei="867064029171533"
#key = calculate_password(each_file_name,imei)
#print(key)

#decrypt = ['/Volumes/DATA/镜像/output/mms/23/data/com.tencent.mm/MicroMsg/818ec766fbdbfffcf788bfa0f069812a/EnMicroMsg_decrypt.db']
#for each_decrypt_db in decrypt:
#    print(each_decrypt_db)
#    wxid = wechat_sqlite.get_wxid(each_decrypt_db)
#    print(wxid)
#    if(wxid == None):
#        print(1)
#    else:
#        print(2)
    
def extract_dbfile(target_dir,wechat_file_list,imei,output_path):
    for each_file_name in wechat_file_list:
        if re.search(r'com.tencent.mm/shared_prefs/auth_info_key_prefs.xml$', each_file_name):
            key = calculate_password(each_file_name,imei)
            print("密钥："+key)
#            print(wechat_file_list)
            decrypt_db_all = []
            for each_file_name2 in wechat_file_list:
                if re.search(r'EnMicroMsg.db$', each_file_name2):
                    decrypt_db = os.path.join(os.path.split(each_file_name2)[0],'EnMicroMsg_decrypt.db')
                    decrypto_dir = [os.path.join(sys.path[0], 'Wechat/sqlcipher_d.exe'),each_file_name2, decrypt_db , key]
                    print(decrypto_dir)
                    try:
                        subprocess.call(decrypto_dir)
                        #os.system(decrypto_dir)
                        decrypt_db_all.append(decrypt_db)
#                        print(decrypt_db)
                    except:
                        None
#                        print(decrypto_dir)
#                    print(os.path.join(output_path, wxid, '.db'))
            print(decrypt_db_all)
            for each_decrypt_db in decrypt_db_all:
                wxid = wechat_sqlite.get_wxid(each_decrypt_db)
                if(wxid == None):
                    continue
                else:
                    output_db_path = os.path.join(output_path, wxid+ '.db')
                    print('开始解析微信id：'+wxid)
                    wechat_sqlite.extract_wechat_troop_msgdata(each_decrypt_db, output_db_path)
                    wechat_sqlite.extract_wechat_troop_members(each_decrypt_db, output_db_path)
                    wechat_sqlite.extract_wechat_personal_msgdata(each_decrypt_db, output_db_path)
                    wechat_sqlite.extract_wechat_personal_friends(each_decrypt_db, output_db_path)
            return wxid+'.db';
#def main(uin_dir,imei_nums,dbfile_dir):
#    key = calculate_password(uin_dir,imei_nums)
#    os.chdir(dbfile_dir)
#    decrypto_dir = 'sqlcipher_d EnMicroMsg.db EnMicroMsg_decrypt.db ' + key
#    os.system(decrypto_dir)
#    wechat_sqlite.extract_wechat_troop_msgdata('EnMicroMsg_decrypt.db','output.db')
#    wechat_sqlite.extract_wechat_troop_members('EnMicroMsg_decrypt.db','output.db')
#    wechat_sqlite.extract_wechat_personal_msgdata('EnMicroMsg_decrypt.db','output.db')
#    wechat_sqlite.extract_wechat_personal_friends('EnMicroMsg_decrypt.db','output.db')


#main('E:\\data\\com.tencent.mm\\shared_prefs\\auth_info_key_prefs.xml','E:\\data\\com.tencent.mobileqq\\files\\imei','E:\\a')

source_img_ranges,source_img_path,output_xml,output_path,guid = Common_ImgRead.read_conf_xml(sys.argv[1])
#try:
imei = read_imei.read_imei(sys.argv[1])
print('获取imei:'+imei)
file_to_get = 'data/com.tencent.mm/'

#调试
#wechat_file_list = ['/Volumes/DATA/镜像/output/23/data/com.tencent.mm/shared_prefs/auth_info_key_prefs.xml', '/Volumes/DATA/镜像/output/mms/23/data/com.tencent.mm/app_webview/databases/Databases.db', '/Volumes/DATA/镜像/output/23/data/com.tencent.mm/MicroMsg/818ec766fbdbfffcf788bfa0f069812a/EnMicroMsg.db']

wechat_file_list = Common_ImgRead.get_file_cache(source_img_ranges,source_img_path,output_path,file_to_get)
#print(output_path,qq_db_list,output_path)
#print(output_path,wechat_file_list,imei,output_path)
output_db_path = extract_dbfile(output_path,wechat_file_list,imei,output_path)
#	输出xml
output_xml_root = ET.Element('Results')
output_xml_guid = ET.SubElement(output_xml_root, 'GUID')
output_xml_guid.text = guid

output_xml_succeed = ET.SubElement(output_xml_root, 'Succeed')
output_xml_succeed.text = 'True'

output_xml_msg = ET.SubElement(output_xml_root, 'Msg')
output_xml_msg.text = ''

output_xml_hasdata = ET.SubElement(output_xml_root, 'HasData')
output_xml_succeed.text = 'True'

output_xml_dataclass = ET.SubElement(output_xml_root, 'DataClass')
output_xml_dataclass.text = 'Chat'

output_xml_dbpath = ET.SubElement(output_xml_root, 'DbPath')
output_xml_dbpath.text = output_db_path

output_xml_imgpath = ET.SubElement(output_xml_root, 'ImgPath')
output_xml_imgpath.text = source_img_path

tree = ET.ElementTree(output_xml_root)
#print(ET.dump(tree))
tree.write(output_xml, encoding='utf-8')