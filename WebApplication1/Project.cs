using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    //Project Model
    public class Project
    {
        
        //A project would have the following information 
        [Key]
        public int ProjectId { get; set; }
        [Required]
        public String projectName { get; set; }
        [Required]
        public DateTime creationDate { get; set; }
        [Required]
        public DateTime expiryDate { get; set; }
        [Required]
        public Boolean enabled { get; set; }
        public List<String> targetCountries { get; set; }
        [Required]
        public Double projectCost { get; set; }
        [Required]
        public String projectURL { get; set; }
       
        //class for targetKeys
        public class targetKeys
        {
            public int number;
            public String keyword;

            //constructor for targetKeys
            public targetKeys()
            {

            }

            public targetKeys(int number,String keyword)
            {
                this.number = number;
                this.keyword = keyword;
            }
            
        }

        //List of targetKeys: Example: [{"number":10,"keyword":"books"},{"number":15,"keyword":"movies"}]
        public List<targetKeys> target_Keys = new List<targetKeys>();


        //returns true if the targetKeys has a number greater than or equal to the given number in the URL else false
        public Boolean getNumbers(int max) {
           foreach (targetKeys target in target_Keys) {
                if (target.number >= max)
                {
                    return true;
                }
            }
            return false; ;
        }

        //returns true if the targetKeys has a keyword equal to the given keyword in the URL else false
        public Boolean getKeyWords(String keyword)
        {
            foreach (targetKeys target in target_Keys)
            {
                if (target.keyword.Equals(keyword))
                {
                    return true;
                }
                
            }
            return false;
        }
    }
}