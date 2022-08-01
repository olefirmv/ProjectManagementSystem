using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;
using PMS.BL.Core;
using System.Reflection;
using System.Drawing;

namespace PMS.BL.Model
{
    [Table(Name = "Stage")]
    public class Stage
    {
        [Column(IsPrimaryKey = true)]
        public int ProjectID { get; set; }

        [Column(IsPrimaryKey = true)]
        public int SectionNum { get; set; }

        [Column(IsPrimaryKey = true)]
        public int StageNum { get; set; }

        [Column(Name = "Status")]
        public int Status { get; set; }

        public Stage(int projectID, int sectionNum, int stageNum, int status)
        {
            ProjectID = projectID;
            SectionNum = sectionNum;
            StageNum = stageNum;
            Status = status;
        }
        public Stage(int projectID, int sectionNum, int stageNum)
        {
            ProjectID = projectID;
            SectionNum = sectionNum;
            StageNum = stageNum;
        }
        public Stage()
        {

        }

        public StageLine AddStageLine(string stageLineName, int documentId, DateTime startDate, DateTime endDate, int employeeId)
        {
            StageDB stageDB = new StageDB();

            int newStageLineId = stageDB.GetNewStageLineId(this);

            StageLine line = new StageLine(
                ProjectID,
                SectionNum,
                StageNum,
                stageDB.GetNewStageLineId(this),
                stageLineName,
                (int)StageLineStatusEnum.Pending,
                documentId,
                startDate,
                endDate,
                employeeId);

            if (line.ValidateWrite())
            {
                StageLineDB stageLineDB = new StageLineDB();

                if (!stageLineDB.InsertMethod(line))
                {
                    return null;
                }
                else
                {
                    line = stageLineDB.SelectMethod<StageLine>().First(x => x.ProjectID == line.ProjectID
                        && x.SectionNum == line.SectionNum
                        && x.StageNum == line.StageNum
                        && x.StageLineID == newStageLineId);
                }
            }

            return line;
        }

        public Color GetBrushStatus()
        {
            Color color = Color.White;
            switch (Status)
            {
                case 1:
                    color = Color.Yellow;
                    break;
                case 2:
                    color = Color.AliceBlue;
                    break;
                case 3:
                    color = Color.Green;
                    break;
                case 4:
                    color = Color.Purple;
                    break;
                case 5:
                    color = Color.Red;
                    break;
            }
            return color;
        }
    }
}
