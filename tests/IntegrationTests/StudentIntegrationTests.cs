using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StudentCrudApi.Dto;
using StudentCrudApi.Students.Model;
using tests.Infrastructure;
using Xunit;

namespace tests.IntegrationTests;

public class StudentIntegrationTests:IClassFixture<ApiWebApplicationFactory>
{
    private readonly HttpClient _client;

    public StudentIntegrationTests(ApiWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }
    
    [Fact]
    public async Task Post_Create_ValidRequest_ReturnsCreatedStatusCode_ValidProductContentResponse()
    {
        var request = "/api/v1/Student/create";
        var student = new CreateStudentRequest() { Name = "new name", Age = 24, Specialization = "new specialization" };
        var content = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Student>(responseString);

        Assert.NotNull(result);
        Assert.Equal(student.Name, result.Name);
        Assert.Equal(student.Age, result.Age);
        Assert.Equal(student.Specialization, result.Specialization);
    }
    
    [Fact]
    public async Task Post_Create_StudentAlreadyExists_ReturnsBadRequestStatusCode()
    {
        var request = "/api/v1/Student/create";
        var student = new CreateStudentRequest() { Name = "new name", Age = 24, Specialization = "new specialization" };
        var content = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");

        await _client.PostAsync(request, content);
        var response = await _client.PostAsync(request, content);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        
    }

    [Fact]
    public async Task Put_Update_ValidRequest_ReturnsAcceptedStatusCode_ValidProductContentResponse()
    {
        var request = "/api/v1/Student/create";
        var student = new CreateStudentRequest() { Name = "new name", Age = 24, Specialization = "new specialization" };
        var content = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Student>(responseString)!;

        request = "/api/v1/Student/update/"+result.Id;
        var updateStudent = new UpdateStudentRequest() { Name = "updated name", Age = 24, Specialization = "updated specialization" };
        content = new StringContent(JsonConvert.SerializeObject(updateStudent), Encoding.UTF8, "application/json");

        response = await _client.PutAsync(request, content);
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        responseString = await response.Content.ReadAsStringAsync();
        result = JsonConvert.DeserializeObject<Student>(responseString)!;

        Assert.Equal(updateStudent.Name, result.Name);
        Assert.Equal(updateStudent.Age, result.Age);
        Assert.Equal(updateStudent.Specialization, result.Specialization);
    }

    [Fact]
    public async Task Put_Update_StudentDoesNotExists_ReturnsNotFoundStatusCode()
    {
        
        var request = "/api/v1/Student/update/1";
        var updateStudent = new UpdateStudentRequest() { Name = "updated name", Age = 24, Specialization = "updated specialization" };
        var content = new StringContent(JsonConvert.SerializeObject(updateStudent), Encoding.UTF8, "application/json");

        var response = await _client.PutAsync(request, content);
        
        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
        
    }

    [Fact]
    public async Task Delete_Delete_StudentExists_ReturnsDeletedStudent()
    {

        var request = "/api/v1/Student/create";
        var student = new CreateStudentRequest() { Name = "new name", Age = 24, Specialization = "new specialization" };
        var content = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Student>(responseString)!;

        request = "/api/v1/Student/delete/" + result.Id;

        response = await _client.DeleteAsync(request);
        
        Assert.Equal(HttpStatusCode.Accepted,response.StatusCode);
    }

    [Fact]
    public async Task Delete_Delete_StudentDoesNotExists_ReturnsNotFoundStatusCode()
    {

        var request = "/api/v1/Student/delete/66";

        var response = await _client.DeleteAsync(request);

        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
        
    }

    [Fact]
    public async Task Get_GetByName_ValidRequest_ReturnsOKStatusCode()
    {

        var request = "/api/v1/Student/create";
        var student = new CreateStudentRequest() { Name = "new name", Age = 24, Specialization = "new specialization" };
        var content = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Student>(responseString)!;

        request = "/api/v1/Student/name/" + result.Name;

        response = await _client.GetAsync(request);

        Assert.Equal(HttpStatusCode.OK,response.StatusCode);

    }

    [Fact]
    public async Task Get_GetByName_StudentDoesNotExists_ReturnsNotFoundStatusCode()
    {

        var request = "/api/v1/Student/name/test";

        var response = await _client.GetAsync(request);
        
        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);

    }
    
    
    
    
}