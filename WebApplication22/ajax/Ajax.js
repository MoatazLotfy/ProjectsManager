function createRequest() //create a request
{
	var MyRequest ;
	if(window.XMLHttpRequest)
	{
		MyRequest = new XMLHttpRequest();
	}
	else
	{
		MyRequest = new ActiveXObject("Microsoft.XMLHTTP");
	}

	return MyRequest;
} // end creatation
//-----------------------------------------------------------------//
function Login() // Login code
{
	MyRequest = createRequest();
	var email = document.getElementById("email").value;
	var password =document.getElementById("password").value;
	MyRequest.onreadystatechange = function(){
		if(this.readyState == 4 && this.status == 200)
		{
			if(this.responseText == "1") alert("Success");
			else alert("Error In Your Data");
		}
		else if (this.readyState < 4 && this.readyState > 0)
		{}
		else
		{
			alert("Error in Requested Page!");
		}
	}
	MyRequest.open("POST","login.php",true);
	MyRequest.setRequestHeader("Content-Type","application/x-www-form-urlencoded");
	MyRequest.send("username="+email+"&"+"pass="+password);
	return false;
} // end Login