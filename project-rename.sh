read -p "Enter CompanyName: " CompanyName
read -p "Enter ProjectName: " ProjectName
read -p "Enter ServiceName: " ServiceName

NewVal="${CompanyName}.${ProjectName}.${ServiceName}";
AtOldVal="ASP.IdentityServer.Core";
UnderLineOldVal="ASP.IdentityServer.Core";

find . -depth -type d -name "${AtOldVal}*" -exec sh -c 'x="{}"; DIR="$(dirname "${x}")" ; FOLDER="$(basename "${x}")"; mv "$x" "${DIR}/${FOLDER/'$AtOldVal'/'$NewVal'}";' \;
echo "Finished Folder Replacement";

find . -type f -name "${AtOldVal}*" -exec sh -c 'x="{}"; DIR="$(dirname "${x}")" ; FILE="$(basename "${x}")"; mv "$x" "${DIR}/${FILE/'$AtOldVal'/'$NewVal'}";' \;
echo "Finished File Replacement";

find ./ -type f -exec sed -i -e 's/'$UnderLineOldVal'/'$NewVal'/g' {} \;
echo "Finished File Content Replacement";

find ./ -type f -exec sed -i -e 's/'$AtOldVal'/'$NewVal'/g' {} \;
echo "Finished File Content SLN Replacement";
echo "DONE";