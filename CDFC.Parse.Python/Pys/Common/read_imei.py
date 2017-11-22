import Common.Common_ImgRead as Common_ImgRead

def read_imei(input_xml):
	source_img_ranges,source_img_path,output_xml,output_path,guid = Common_ImgRead.read_conf_xml(input_xml)
	file_to_get = 'com.tencent.mobileqq/files/imei'
	file_cache = Common_ImgRead.get_file_cache(source_img_ranges,source_img_path,output_path,file_to_get)
	#print(file_cache)

	with open(file_cache[0], 'r') as file_imei:
		for file_imei_line in file_imei.readlines():
	#		print(file_imei_line)
			if file_imei_line.find('imei=')+1:
				return(file_imei_line[5:].strip('\n'))