using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace SystemDemonstrator
{
    class c_Storage
    {
        public List<c_StoragePlace> m_ListOfStoragePlaces;
        c_Fabric m_cFabric;
        public c_Storage(c_Fabric cFabric)
        {
            m_cFabric = cFabric;
            m_ListOfStoragePlaces = new List<c_StoragePlace>();
            CommonFunctions Reader = new CommonFunctions();
            string sJSon = "";
            string sPathOfStorage;
            
                sPathOfStorage = c_Config.m_sStorageFile;

            Reader.ReadJSonFile(sPathOfStorage, ref sJSon);

            m_ListOfStoragePlaces = JsonConvert.DeserializeObject<List<c_StoragePlace>>(sJSon);
         
        }

        public List<c_StoragePlace> GetStorageLocationList()
        {
            return m_ListOfStoragePlaces;
        }

        public int GetPlaceOfID(string sWPID)
        {

            int iStoragePlace = 0;

            List<c_StoragePlace> ListOfPlaces = GetStorageLocationList();

            foreach (var Place in ListOfPlaces)
            {
                if (Place.m_iWorkpieceID == sWPID)
                {
                    iStoragePlace = Place.m_iNumber;
                    break;
                }
            }
            return iStoragePlace;
        }
        public int GetPlaceOfFirstAvailable()
        {

            int iStoragePlace = 0;

            List<c_StoragePlace> ListOfPlaces = GetStorageLocationList();

            foreach (var Place in ListOfPlaces)
            {
                if (Place.m_iWorkpieceID == "Empty")
                {
                    iStoragePlace = Place.m_iNumber;
                    return iStoragePlace;

                }
            }
            return 0;

        }
        public void SetPlaceEmpty(int iNo)
        {
            foreach (var Place in m_ListOfStoragePlaces)
            {
                if (Place.m_iNumber == iNo)
                {
                    Place.m_bEmpty = true;
                }
            }
        }
        public bool IsPlaceEmpty(int iNo)
        {
            foreach (var Place in m_ListOfStoragePlaces)
            {
                if (Place.m_iNumber == iNo)
                {
                    return Place.m_bEmpty;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public void SetPlaceOccupied(int iNo)
        {
            foreach (var Place in m_ListOfStoragePlaces)
            {
                if (Place.m_iNumber == iNo)
                {
                    Place.m_bEmpty = false;
                }
            }
        }

        public bool GetStoragePlace(int iNo, ref c_StoragePlace cStoragePlace)
        {
            foreach (var Place in m_ListOfStoragePlaces)
            {
                if (Place.m_iNumber == iNo)
                {
                    cStoragePlace = Place;
                    return true;
                }
            }
            return false;
        }
        public bool GetStoragePlaceOfID(String sID, ref c_StoragePlace cStoragePlace)
        {
            foreach (var Place in m_ListOfStoragePlaces)
            {
                if (Place.m_iWorkpieceID == sID)
                {
                    cStoragePlace = Place;
                    return true;
                }
            }
            return false;
        }

        public string GetStorageIDOfPlace(int iNum)
        {
            foreach (var Place in m_ListOfStoragePlaces)
            {
                if (Place.m_iNumber == iNum)
                {
                    return Place.m_iWorkpieceID;
                }
            }
            return "Empty";
        }
    }
}
