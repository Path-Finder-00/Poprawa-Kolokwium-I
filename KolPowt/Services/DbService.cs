using KolPowt.Models;
using KolPowt.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace KolPowt.Services
{
    public class DbService : IDbService
    {
        private string _connectionString = "Data Source=(localDb)\\localDb;Initial Catalog=localhost;Integrated Security=True";

        public GetMedicament GetMedicament(int id)
        {
            GetMedicament Med = null;
            var ids = new List<int>();
            var Prescriptions = new List<Prescription>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandText = $"SELECT IdPrescription FROM Prescription_Medicament WHERE IdMedicament = {id}";
                con.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    ids.Add(Int32.Parse(dr["IdPrescription"].ToString()));
                }
                dr.Close();

                string nums = "(";
                foreach (var num in ids)
                {
                    nums += num.ToString() + ", ";
                }
                nums = nums.Substring(0, nums.Length - 2);
                nums += ")";
                Console.WriteLine(nums);
                SqlCommand com2 = new SqlCommand();
                com2.Connection = con;
                com2.CommandText = $"SELECT * FROM Prescription WHERE IdPrescription IN {nums}";
                var dr2 = com2.ExecuteReader();
                while (dr2.Read())
                {
                    Prescriptions.Add(new Prescription
                    {
                        IdPrescription = Int32.Parse(dr2["IdPrescription"].ToString()),
                        Date = DateTime.Parse(dr2["Date"].ToString()),
                        DueDate = DateTime.Parse(dr2["DueDate"].ToString()),
                        IdPatient = Int32.Parse(dr2["IdPatient"].ToString()),
                        IdDoctor = Int32.Parse(dr2["IdDoctor"].ToString())
                    });
                }

                dr2.Close();

                Prescriptions.Sort((p, q) => p.Date.CompareTo(q.Date));

                SqlCommand com3 = new SqlCommand();
                com3.Connection = con;
                com3.CommandText = $"SELECT * FROM Medicament WHERE IdMedicament = {id}";

                var dr3 = com3.ExecuteReader();
                while (dr3.Read())
                {
                    Med = new GetMedicament
                    {
                        IdMedicament = Int32.Parse(dr3["IdMedicament"].ToString()),
                        Name = dr3["Name"].ToString(),
                        Description = dr3["Description"].ToString(),
                        Type = dr3["Type"].ToString(),
                        Prescriptions = Prescriptions
                    };
                }
                dr3.Close();
            }
            return Med;
        }

        public bool DeletePatient(int id)
        {

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                var transaction = con.BeginTransaction();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.Transaction = transaction as SqlTransaction;
                try
                {
                    var PrescriptionsIds = new List<int>();

                    com.CommandText = $"SELECT IdPrescription FROM Prescription WHERE IdPatient = {id}";
                    

                    var dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        PrescriptionsIds.Add(Int32.Parse(dr["IdPrescription"].ToString()));
                    }
                    dr.Close();

                    foreach (var Prescription in PrescriptionsIds)
                    {
                        com.CommandText = $"DELETE From Prescription_Medicament WHERE IdPrescription = {PrescriptionsIds}";
                        com.ExecuteScalar();
                    }

                    com.CommandText = $"DELETE FROM Prescription WHERE IdPatient = {id}";
                    com.ExecuteScalar();

                    com.CommandText = $"DELETE FROM Patient WHERE IdPatient = {id}";
                    com.ExecuteScalar();

                    transaction.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    throw new SystemException(ex.Message);
                }

            }
        }
    }
}

