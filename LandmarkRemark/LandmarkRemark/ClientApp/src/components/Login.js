import React, { Component } from 'react';
import {Paper, Typography} from "@material-ui/core";
import TextField from "@material-ui/core/TextField";
import Button from "@material-ui/core/Button";

export class Login extends Component {
  static displayName = Login.name;

  constructor (props) {
    super(props);
    this.state = {
      username: '',
      password: '',
      buttonText: 'Login'
    };
  }

  handleChange = name => event => {
    this.setState({
      [name]: event.target.value,
    });
  };
  
  handleError = () => {
    this.setState({buttonText: 'Error'})
  };
  
  handleLogin = () => {
    
    this.setState({
      buttonText: 'Loading...'
    });
    
    // send login request, get back token
    fetch('api/Auth/Login', {
      method: 'post',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        username: this.state.username,
        password: this.state.password
      })
    }).then(res => res.json())
      .then((res) => {
        if(res.status === 401) {
          this.handleError();
          return;
        }
        
        // store token and user data in localstorage for sending requests
        localStorage.setItem('userData', JSON.stringify({
          username: this.state.username,
          token: res.token
        }));
        
        // I need to learn react
        window.location.reload()
      });
  };

  render () {
    return (
      <div className="login-container">
        <Typography variant="h6" gutterBottom>
          Login to Landmark Remarks
        </Typography>

        <TextField
          id="outlined-user"
          label="Username"
          value={this.state.username}
          onChange={this.handleChange('username')}
          margin="normal"
          variant="outlined"
        />
        <br/>
        <TextField
          id="outlined-pass"
          label="Password"
          value={this.state.password}
          onChange={this.handleChange('password')}
          margin="normal"
          type="password"
          autoComplete="current-password"
          variant="outlined"
        />
        <br/>
        <Button variant="contained" color="primary" onClick={this.handleLogin}>
          {this.state.buttonText}
        </Button>
      </div>
    );
  }
}
