using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;
using PMS.BL.Core;
using System.Drawing;

namespace PMS.BL.Model
{
    [Table(Name = "Section")]
    public class Section
    {
        [Column(IsPrimaryKey = true)]
        public int ProjectID { get; set; }

        [Column(IsPrimaryKey = true)]
        public int SectionNum { get; set; }
                
        [Column(Name = "Cipher")]
        public string Cipher { get; set; }

        public string SectionName
        {
            get
            {
                return GetSectionName();
            }
        }

        public Section(int projectID, int sectionEnum, string cipher)
        {
            ProjectID = projectID;
            SectionNum = sectionEnum;
            Cipher = cipher;
        }

        public Section()
        {

        }

        public string GetSectionName()
        {
            SectionEnum sectionEnum = (SectionEnum)SectionNum;
            return PMSBLHelper.GetDescriptionByEnumType(sectionEnum);
        }

        public void InitStages()
        {
            List<Stage> stages = PMSBLHelper.GetStagesBySection(this);

            DataBase dataBase = new DataBase();
            dataBase.InsertMethod(stages);
        }


        public void InitSRWStages(List<bool> boolListStageStatusBySection)
        {
            List<Stage> stages = new List<Stage>();
            stages = this.ConfigureSRWStages(boolListStageStatusBySection);

            DataBase dataBase = new DataBase();
            dataBase.InsertMethod(stages);
        }

        public void InitEDWStages(List<string> selectedEDWItems)
        {
            List<Stage> stages = new List<Stage>();
            stages = this.ConfigureEDWStages(selectedEDWItems);

            DataBase dataBase = new DataBase();
            dataBase.InsertMethod(stages);
        }

        public bool ValidateWrite()
        {
            return this.Cipher != null && this.Cipher != string.Empty;
        }

        private List<Stage> ConfigureSRWStages(List<bool> boolListStageStatusBySection)
        {
            List<Stage> result = new List<Stage>();
            for (int count = 0; count < boolListStageStatusBySection.Count; count++)
            {
                int stageNum = count + 1;
                if (boolListStageStatusBySection[count] == true)
                {
                    result.Add(new Stage(this.ProjectID, this.SectionNum, stageNum, (int)StageStatusEnum.Pending));
                }
                else
                {
                    result.Add(new Stage(this.ProjectID, this.SectionNum, stageNum, (int)StageStatusEnum.Missing));
                }

            }
            return result;
        }

        private List<Stage> ConfigureEDWStages(List<string> input)
        {
            List<Stage> res = new List<Stage>();
            foreach (var item in input)
            {
                List<Stage> ret = ParseStringToEDWStageAndStatuses(item);
                foreach (var stage in ret)
                {
                    res.Add(stage);
                }
            }
            return res;
        }

        public List<Stage> ParseStringToEDWStageAndStatuses(string value)
        {
            List<Stage> resList = new List<Stage>();

            switch (value)
            {
                case "Разработка ЭП и Разработка ТП объединены":
                    resList.Add(new Stage(this.ProjectID, this.SectionNum, 1, (int) StageStatusEnum.Merged));
                    resList.Add(new Stage(this.ProjectID, this.SectionNum, 2, (int)StageStatusEnum.Pending));
                    break;
                case "Разработка ЭП, Разработка ТП раздельно":
                    resList.Add(new Stage(this.ProjectID, this.SectionNum, 1, (int)StageStatusEnum.Pending));
                    resList.Add(new Stage(this.ProjectID, this.SectionNum, 2, (int)StageStatusEnum.Pending));
                    break;
                case "Разработка ТП (Разработка ЭП исключен)":
                    resList.Add(new Stage(this.ProjectID, this.SectionNum, 1, (int)StageStatusEnum.Missing));
                    resList.Add(new Stage(this.ProjectID, this.SectionNum, 2, (int)StageStatusEnum.Pending));
                    break;
                case "Этапы, начиная с Разработки ЭП, заканчивая Изготовлением ОО изделия ВТ и проведение ПИ объединены":
                    resList.Add(new Stage(this.ProjectID, this.SectionNum, 1, (int)StageStatusEnum.Merged));
                    resList.Add(new Stage(this.ProjectID, this.SectionNum, 2, (int)StageStatusEnum.Merged));
                    resList.Add(new Stage(this.ProjectID, this.SectionNum, 3, (int)StageStatusEnum.Merged));
                    resList.Add(new Stage(this.ProjectID, this.SectionNum, 4, (int)StageStatusEnum.Pending));
                    break;
                case "Разработка РКД для изготовления ОО изделия ВТ, Изготовление ОО изделия ВТ и проведение ПИ раздельно":
                    resList.Add(new Stage(this.ProjectID, this.SectionNum, 3, (int)StageStatusEnum.Pending));
                    resList.Add(new Stage(this.ProjectID, this.SectionNum, 4, (int)StageStatusEnum.Pending));
                    break;
                case "Разработка РКД для изготовления ОО изделия ВТ, Изготовление ОО изделия ВТ и проведение ПИ объединены":
                    resList.Add(new Stage(this.ProjectID, this.SectionNum, 3, (int)StageStatusEnum.Merged));
                    resList.Add(new Stage(this.ProjectID, this.SectionNum, 4, (int)StageStatusEnum.Pending));
                    break;
                case "Проведение ГИ ОО изделия ВТ, Утверждение РКД для организации промышленного (серийного) производства изделий ВТ раздельно":
                    resList.Add(new Stage(this.ProjectID, this.SectionNum, 5, (int)StageStatusEnum.Pending));
                    resList.Add(new Stage(this.ProjectID, this.SectionNum, 6, (int)StageStatusEnum.Pending));
                    break;
                case "Проведение ГИ ОО изделия ВТ, Утверждение РКД для организации промышленного (серийного) производства изделий ВТ объединены":
                    resList.Add(new Stage(this.ProjectID, this.SectionNum, 5, (int)StageStatusEnum.Merged));
                    resList.Add(new Stage(this.ProjectID, this.SectionNum, 6, (int)StageStatusEnum.Pending));
                    break;
                case "":
                    break;
            }
            return resList;
        }
        public List<Stage> GetStages()
        {
            return new DataBase().SelectMethod<Stage>().Where(s => s.SectionNum == this.SectionNum && s.ProjectID == this.ProjectID).ToList();
        }
        public List<Color> GetSectionColorList()
        {
            List<Color> result = new List<Color>();

            foreach (var stage in GetStages())
            {
                Color color = stage.GetBrushStatus();

                result.Add(color);
            }
            
            return result;
        }



    }

}
