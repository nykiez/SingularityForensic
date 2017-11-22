import os,re,shutil,sys
from Phone_msg_calllog.Phone_msg_calllog import extract_phone_call_log
from Phone_msg_calllog.Phone_msg_calllog import extract_raw_contact
from Phone_msg_calllog.Phone_msg_calllog import extract_contact_data
from Phone_msg_calllog.Phone_msg_calllog import extract_phone_sms
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

def extract_dbfile(target_dir, contact_to_get, mmssms_db_list, output_path):
	output_ccs_filename = 'calllog_contact_sms.db'
	output_db = os.path.join(output_path, output_ccs_filename)
#	print(contact_to_get)
	for each_file_name in contact_to_get:
		if re.search(r'com.android.providers.contacts/databases/contacts2.db$', each_file_name):
#			print(each_file_name)
			extract_phone_call_log(each_file_name ,output_db)
			extract_raw_contact(each_file_name ,output_db)
			extract_contact_data(each_file_name ,output_db)
	for each_file_name in mmssms_db_list:
#		print(each_file_name)
		if re.search(r'com.android.providers.telephony/databases/mmssms.db$', each_file_name):
#			print(each_file_name)
			extract_phone_sms(each_file_name ,output_db)
	return output_db


#python  qq_extract.py  dddd.xml
#for qq_db_file in qq_db_list:
#	if re.search(r'com.tencent.mobileqq/databases/\d*.db$', qq_db_file):

#global IMEI
source_img_ranges,source_img_path,output_xml,output_path,guid = Common_ImgRead.read_conf_xml(sys.argv[1])
#try:
#imei = read_imei.read_imei(sys.argv[1])
#print('获取imei:'+imei)
contact_to_get = 'contacts2.db'
contacts_db_list = Common_ImgRead.get_file_cache(source_img_ranges,source_img_path,output_path,contact_to_get)

mmsfile_to_get = 'mmssms.db'
mmssms_db_list = Common_ImgRead.get_file_cache(source_img_ranges,source_img_path,output_path,mmsfile_to_get)
#print(output_path,mmssms_db_list,output_path)

#print(file_to_get)
output_db_path = extract_dbfile(output_path,contacts_db_list,mmssms_db_list,output_path)
##	输出xml
output_xml_root = ET.Element('Results')
output_xml_guid = ET.SubElement(output_xml_root, 'GUID')
output_xml_guid.text = guid
#
output_xml_succeed = ET.SubElement(output_xml_root, 'Succeed')
output_xml_succeed.text = 'True'
#
output_xml_msg = ET.SubElement(output_xml_root, 'Msg')
output_xml_msg.text = ''
#
output_xml_hasdata = ET.SubElement(output_xml_root, 'HasData')
output_xml_succeed.text = 'True'
#
output_xml_dataclass = ET.SubElement(output_xml_root, 'DataClass')
output_xml_dataclass.text = 'Chat'
#
output_xml_dbpath = ET.SubElement(output_xml_root, 'DbPath')
output_xml_dbpath.text = output_db_path
#
output_xml_imgpath = ET.SubElement(output_xml_root, 'ImgPath')
output_xml_imgpath.text = source_img_path
#

#
tree = ET.ElementTree(output_xml_root)
#print(ET.dump(tree))
tree.write(output_xml, encoding='utf-8')
#except:
	#print('Error!')