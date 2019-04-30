import React, {Component} from 'react';
import {Route} from 'react-router';
import {Layout} from './components/Layout';
import {Home} from './components/Home';
import {Remarks} from './components/Remarks';
import {Login} from './components/Login';
import "./main.css";
import CircularProgress from '@material-ui/core/CircularProgress';

export default class App extends Component {
  static displayName = App.name;

  constructor(props) {
    super(props);
    
    let username = this.getUsername();
    let loggedIn = username != null;

    this.state = {
      remarks: [], 
      loading: true,
      loggedIn: loggedIn
    };

    // get the data from our api
    fetch('api/Landmark/GetAllRemarks')
      .then(response => response.json() )
      .then(data => {
        this.setState({remarks: data, loading: false});
      });
  }
  
  // get username from localstorage
  getUsername() {
    let userData = localStorage.getItem('userData');
    
    if(!userData) return null;
    
    let parsed = JSON.parse(userData);
    
    if(!parsed) return null;
    
    return parsed.username;
  }

  // render the layout displayed to a logged in user
  renderLayout() {
    return (
      <Layout loggedIn={this.state.loggedIn}>
        <Route exact path='/' render={(props) => <Home {...props} remarks={this.state.remarks}/>}/>
        <Route path='/remarks' render={(props) => <Remarks {...props} remarks={this.state.remarks}/>}/>
      </Layout>
    );
  }
  
  renderLoading() {
    return (
      <div className="loading-screen">
        <CircularProgress color="primary" />
      </div>
    );
  }

  // render the layout displayed to a non-logged in user
  renderLogin() {
    return (
      <Layout loggedIn={this.state.loggedIn}>
        <Route exact path='/' component={Login}/>
      </Layout>
    );
  }

  render() {
    
    if(this.state.loading) {
      return (
        <div>
          {this.renderLoading()}
        </div>
      )
    }

    let contents = this.state.loggedIn ? this.renderLayout() : this.renderLogin();

    return (
      <div>
        {contents}
      </div>
    );
  }
}
