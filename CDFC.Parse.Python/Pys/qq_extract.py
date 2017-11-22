import os,re,shutil,sys
import QQ.sqliteqq as sqliteqq
import Common.read_imei as read_imei
import Common.Common_ImgRead as Common_ImgRead
import xml.etree.ElementTree as ET


def search_file(start_dir,file_name):
	os.chdir(start_dir)
	for each_file in os.listdir(os.curdir):
		if each_file != file_name:
			continue
		else:
			file_dir = os.path.abspath(file_name)
			return file_dir#返回绝对路径

def extract_dbfile(target_dir,qq_db_list,imei,output_path):
	for each_file_name in qq_db_list:
		if re.search(r'com.tencent.mobileqq/databases/\d*.db$', each_file_name):
			qq_number = os.path.splitext(os.path.basename(each_file_name))[0]
			output_db_filename ='qq_' + qq_number + '.db'
			output_db = os.path.join(output_path, output_db_filename)
			print('获取QQ号:' + qq_number +'聊天记录')
			sqliteqq.extract_qq_personal_msgdata(each_file_name,qq_number,imei,output_db)
			print('获取QQ号:' + qq_number +'联系人信息')
			sqliteqq.extract_qq_personal_friends(each_file_name,qq_number,imei,output_db)
			print('获取QQ号:' + qq_number +'群聊天记录')
			sqliteqq.extract_qq_troop_msgdata(each_file_name,qq_number,imei,output_db)
			print('获取QQ号:' + qq_number +'群成员信息')
			sqliteqq.extract_qq_troop_members(each_file_name,qq_number,imei,output_db)
#			print('获取QQ号:' + qq_number +'讨论组聊天记录')
#			sqliteqq.extract_qq_discussion_msgdata(each_file_name,qq_number,imei,output_db)
#			print('获取QQ号:' + qq_number +'讨论组信息')
#			sqliteqq.extract_qq_discussion_members(each_file_name,qq_number,imei,output_db)
			return output_db_filename


#python  qq_extract.py  dddd.xml
#for qq_db_file in qq_db_list:
#	if re.search(r'com.tencent.mobileqq/databases/\d*.db$', qq_db_file):

#global IMEI
source_img_ranges,source_img_path,output_xml,output_path,guid = Common_ImgRead.read_conf_xml(sys.argv[1])
#try:
imei = read_imei.read_imei(sys.argv[1])
print('获取imei:'+imei)
file_to_get = 'com.tencent.mobileqq/databases/'
qq_db_list = Common_ImgRead.get_file_cache(source_img_ranges,source_img_path,output_path,file_to_get)
#print(output_path,qq_db_list,output_path)
output_db_path = extract_dbfile(output_path,qq_db_list,imei,output_path)
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
#except:
	#print('Error!')