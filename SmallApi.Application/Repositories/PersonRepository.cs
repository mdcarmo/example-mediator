using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SmallApi.Application.Entities;
using SmallApi.Application.Infra;
using SmallApi.Application.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SmallApi.Application.Repositories
{
    public class PersonRepository : BaseSqlServerDao, IPersonRepository
    {
        /// <summary>
        /// TODO:Documentar
        /// </summary>
        /// <param name="appSettings"></param>
        /// <param name="env"></param>
        public PersonRepository(IOptions<SqlServerSettings> appSettings, IHostingEnvironment env) : base(appSettings, env)
        {
            this.DefaultQuery = "SELECT * FROM TPI_PERSON WHERE 1 = 1";
            this.DefaultKeyQuery = "SELECT ID, FIRSTNAME, LASTNAME, AGE, TYPE, ACTIVE, USERNAME, PHONE, EMAIL, REGISTER, PHOTO, AREA, DATEREGISTER FROM TPI_PERSON WHERE 1 = 1";
            this.DefaultInsert = @"INSERT INTO TPI_PERSON (FirstName, LastName, Age, Type, Active, Username, Phone, Email, Register, Photo, Area, DateRegister) 
                                     VALUES (@FirstName, @LastName, @Age, @Type, @Active, @UserName, @Phone, @Email, @Register, @Photo, @Area, @DateRegister)
                                     ; SELECT CAST(SCOPE_IDENTITY() as int)";
            this.DefaultUpdate = @"UPDATE TPI_PERSON SET FirstName = @FirstName, LastName = @LastName, Age = @Age,
                                            Type = @Type, Active = @Active, Username = @Username, Phone = @Phone, Email = @Email, Register = @Register,
                                            Photo = @Photo, Area = @Area 
                                            WHERE ID = @ID";
            this.DefaultDelete = "DELETE TPI_PERSON WHERE ID = @ID";
        }

        public async Task<IEnumerable<Person>> GetAll()
        {
            using (SqlConnection dbCon = DbConnection)
            {
                if (dbCon.State == ConnectionState.Closed)
                    dbCon.Open();

                return await dbCon.QueryAsync<Person>(DefaultQuery);
            }
        }
        public async Task<Person> GetById(object id)
        {
            using (SqlConnection dbCon = DbConnection)
            {
                if (dbCon.State == ConnectionState.Closed)
                    dbCon.Open();

                var resultDb = await dbCon.QueryAsync<Person>(DefaultKeyQuery + " AND ID = @ID", new { ID = id });
                return resultDb.FirstOrDefault();
            }
        }
        public async Task<int> Create(Person entity)
        {
            using (SqlConnection dbCon = DbConnection)
            {
                if (dbCon.State == ConnectionState.Closed)
                    dbCon.Open();

                var param = new DynamicParameters();
                param.Add(name: "FirstName", value: entity.FirstName, direction: ParameterDirection.Input);
                param.Add(name: "LastName", value: entity.LastName, direction: ParameterDirection.Input);
                param.Add(name: "Age", value: entity.Age, direction: ParameterDirection.Input);
                param.Add(name: "Type", value: entity.Type, direction: ParameterDirection.Input);
                param.Add(name: "Active", value: entity.Active, direction: ParameterDirection.Input);
                param.Add(name: "UserName", value: entity.UserName, direction: ParameterDirection.Input);
                param.Add(name: "Phone", value: entity.Phone, direction: ParameterDirection.Input);
                param.Add(name: "Email", value: entity.Email, direction: ParameterDirection.Input);
                param.Add(name: "Register", value: entity.Register, direction: ParameterDirection.Input);
                param.Add(name: "Photo", value: entity.Photo, direction: ParameterDirection.Input);
                param.Add(name: "Area", value: entity.Area, direction: ParameterDirection.Input);
                param.Add(name: "DateRegister", value: entity.DateRegister, direction: ParameterDirection.Input);

                return await dbCon.QueryFirstOrDefaultAsync<int>(DefaultInsert, param);
            }
        }
        public async Task<bool> Update(Person entity)
        {
            using (SqlConnection dbCon = DbConnection)
            {
                if (dbCon.State == ConnectionState.Closed)
                    dbCon.Open();

                DynamicParameters param = new DynamicParameters();
                param.Add("ID", entity.ID);
                param.Add("FirstName", entity.FirstName);
                param.Add("LastName", entity.LastName);
                param.Add("Age", entity.Age);
                param.Add("Type", entity.Type);
                param.Add("Active", entity.Active);
                param.Add("UserName", entity.UserName);
                param.Add("Phone", entity.Phone);
                param.Add("Email", entity.Email);
                param.Add("Register", entity.Register);
                param.Add("Photo", entity.Photo);
                param.Add("Area", entity.Area);

                return await dbCon.ExecuteAsync(DefaultUpdate, param) > 0;
            }
        }
        public async Task<bool> Delete(int id)
        {
            using (SqlConnection dbCon = DbConnection)
            {
                if (dbCon.State == ConnectionState.Closed)
                    dbCon.Open();

                var parameters = new DynamicParameters();
                parameters.Add("ID", id);

                return await dbCon.ExecuteAsync(DefaultDelete, parameters) > 0;
            }
        }
        public async Task<bool> ExistWithThisRegister(int register)
        {
            using (SqlConnection dbCon = DbConnection)
            {
                if (dbCon.State == ConnectionState.Closed)
                    dbCon.Open();

                return await dbCon.ExecuteScalarAsync<bool>("SELECT COUNT(1) FROM TPI_PERSON WHERE REGISTER = @Register", new { register });
            }
        }
    }
}
