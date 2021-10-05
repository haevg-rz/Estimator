using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Estimator.Data
{
    public class TShirtVoteResult
    {
        public int SizeS { get; set; }
        public int SizeM { get; set; }
        public int SizeL { get; set; }
        public int SizeXL { get; set; }
        public int SizeXXL { get; set; }

        public TShirtVoteResult()
        {
            this.SizeS = 0;
            this.SizeM = 0;
            this.SizeL = 0;
            this.SizeXL = 0;
            this.SizeXXL = 0;
        }
        public TShirtVoteResult(int sizeS,int sizeM,int sizeL, int sizeXL, int sizeXXL)
        {
            this.SizeS = sizeS;
            this.SizeM = sizeM;
            this.SizeL = sizeL;
            this.SizeXL = sizeXL;
            this.SizeXXL = sizeXXL;
        }

        public void AdminStartTShirtVoting()
        {
            this.SizeS = 0;
            this.SizeM = 0;
            this.SizeL = 0;
            this.SizeXL = 0;
            this.SizeXXL = 0;
        }

        public void TShirtVoteResultbyVoter(string tShirtSize)
        {
            switch (tShirtSize)
            {
                case "sizeS":
                    this.SizeS ++;
                    break;
                case "sizeM":
                    this.SizeM ++;
                    break;
                case "sizeL":
                    this.SizeL ++;
                    break;
                case "sizeXL":
                    this.SizeXL ++;
                    break;
                case "sizeXXL":
                    this.SizeXXL ++;
                    break;
            }

        }
    }
}
