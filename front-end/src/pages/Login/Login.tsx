import {useState} from 'react';
import { useNavigate, Link } from 'react-router-dom';
import "./Login.css";
import UsernameField from 'components/UsernameField';
import PasswordField from 'components/PasswordField';

export default function Login() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const [usernameValidator, setUsernameValidator] = useState("");
  const [passwordValidator, setPasswordValidator] = useState("");

  const validateFields = () =>{
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
  }
  const handleSubmit = (event: { preventDefault: () => void }) =>{
    event.preventDefault();
    let fieldsAreValid = validateFields();

    if(fieldsAreValid){
      // fetch POST
      if(true /*fetch response is ok*/){
        console.log("login fetch was good")
        // save something in session storage
        // navigate("/");
      }
      else{
        console.log("login fetch was bad");
        // set username validator to response message
      } 
    }
    else{
    console.log("login was bad");
  }
  }
  const handleUsernameChange = (newUsername: React.ChangeEvent<HTMLInputElement>)=>{
    setUsername(newUsername.target.value);
  }
  const handlePasswordChange = (newPassword : React.ChangeEvent<HTMLInputElement>)=>{
    setPassword(newPassword.target.value);
  }
  return (
    <>
    <form onSubmit={handleSubmit}>
      <div className="column-wrapper">
        <UsernameField
          usernameValidator={usernameValidator}
          username={username}
          handleUsernameChange={handleUsernameChange}
        />
        <PasswordField
          passwordValidator={passwordValidator}
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