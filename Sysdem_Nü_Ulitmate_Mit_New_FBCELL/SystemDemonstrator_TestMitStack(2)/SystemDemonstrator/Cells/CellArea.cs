using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SystemDemonstrator.ADSInterface;
using System.Windows.Input;
using SystemDemonstrator.Commands;

namespace SystemDemonstrator
{
    class c_CellArea : c_Base_ADSReadInterface
    {
         public List<c_Cell> m_ListOfCells { get; set; }
        c_Fabric m_cFabric;

        public c_CellArea(c_Fabric cFabric)
        {
            m_cFabric = cFabric;
            CommonFunctions Reader = new CommonFunctions();

            ADSReadJSon = new ADSReadInterface("MAIN.sJsonDocCells");
            m_ListOfCells = new List<c_Cell>();

            ADSReadJSon.Registrate((c_Base_ADSReadInterface)this);
            
        }

        public List<c_Cell> GetCellList()
        {
            return m_ListOfCells;
        }

        public bool GetCellOfName(String sCellName, ref c_Cell cCell)
        {
            
            foreach (var Cell in m_ListOfCells)
            {
                if (Cell.Name == sCellName)
                {
                    cCell = Cell;
                    return true;
                }
                    
            }
            return false;

        }
        public bool IsCellVirtualByName(String sCellName)
        {

            foreach (c_Cell Cell in m_ListOfCells)
            {
                if (Cell.Name == sCellName)
                {
                    return Cell.IsVirtualCell;
                }

            }
            return false;

        }
        public int GetCellNumberOfName(String sCellName)
        {
            int iNumber = 1;
            foreach (var Cell in m_ListOfCells)
            {
                if (Cell.Name == sCellName)
                {
                    return iNumber;
                }
                iNumber++;
            }
            return 0;

        }
        public bool SearchWPInCells(int sWPID, ref c_Cell cCell)
        {
            foreach (var Cell in m_ListOfCells)
            {
                if (Cell.Workpiece == sWPID)
                {
                    cCell = Cell;
                    return true;
                }

            }
            return false;

        }

        public override void Update(string sJSon)
        {
      
               
                c_Cell Cell = JsonConvert.DeserializeObject<c_Cell>(sJSon);
                bool b = false;
                int i = 0;
                if (m_ListOfCells != null)
                { 

                foreach (c_Cell ListCell in m_ListOfCells)
                {

                    if (ListCell.Name == Cell.Name)
                    {
                        b = true;
                        break;
                    }
                    i++;
                }
                if (b)
                    m_ListOfCells[i] = Cell;
                else
                    m_ListOfCells.Add(Cell);
                }
                else
                {
                    m_ListOfCells.Add(Cell);
                }
            //m_cMainView.Invoke((MethodInvoker)delegate { UpdateChart(); });
         
                m_cFabric.update();
            }

        }
        
    }

