import {useState} from 'react';
import { useNavigate, Link } from 'react-router-dom';
import "./Login.css";
import UsernameField from 'components/UsernameField';
import PasswordField from 'components/PasswordField';

export default function Login() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const [validator, setValidator] = useState("");

  const navigate = useNavigate();

  /*const validateFields = () =>{
    let fieldsAreValid = true;

    setUsernameValidator("");
    setPasswordValidator("");

    if(username === ""){
      setUsernameValidator("Username can't be empty")
      fieldsAreValid = false;
    }
    if(password === ""){
      setPasswordValidator("Password can't be empty");
      fieldsAreValid = false;
    }

    return fieldsAreValid;
  }*/
  const handleSubmit = (event: { preventDefault: () => void }) =>{
    event.preventDefault();

      fetch("https://localhost:7231/Account/Login",{
        method: "POST",
        body: JSON.stringify({username, password}),
        headers:{
          "Content-Type": "application/json"
        }
      })
      .then(async (response)=>{
        if(response.status === 200){
          sessionStorage.setItem("myId", await response.json());
          navigate("/");
          window.location.reload();
        }
        else{
          setValidator(await response.text());
        }
      })
      .catch((error)=>console.error(error));
  }
  const handleUsernameChange = (newUsername: React.ChangeEvent<HTMLInputElement>)=>{
    setUsername(newUsername.target.value);
  }
  const handlePasswordChange = (newPassword : React.ChangeEvent<HTMLInputElement>)=>{
    setPassword(newPassword.target.value);
  }
  return (
    <>
    <h1>LOGIN</h1>
    <form onSubmit={handleSubmit}>
      <div className="column-wrapper">
      {
            validator !== "" ?
            (<label style={{color: "red"}}>* {validator}</label>)
            : ""
          }
        <UsernameField
          username={username}
          handleUsernameChange={handleUsernameChange}
        />
        <PasswordField
          password={password}
          handlePasswordChange={handlePasswordChange}
        />
      </div>
      <input type={"submit"} value={"Login"}/>
    </form>
    <Link to={"/register"}>
      Don't have an account? Click here!
    </Link>
    </>
    );
  }