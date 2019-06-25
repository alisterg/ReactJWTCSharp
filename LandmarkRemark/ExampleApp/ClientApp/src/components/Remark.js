import React, {Component} from 'react';
import Typography from "@material-ui/core/Typography";
import {Divider} from "@material-ui/core";

export class Remark extends Component {
  static displayName = Remark.name;

  constructor(props) {
    super(props);
    
    let dateObj = new Date(this.props.remark.dateCreated);
    const options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric', hour: 'numeric', minute: 'numeric' };
    let dateStr = dateObj.toLocaleDateString("en-AU", options);
    
    // get current user to check if we highlight the border
    let userData = localStorage.getItem('userData');
    userData = JSON.parse(userData);

    this.state = {
      isCurrentUser: this.props.remark.username === userData.username,
      id: this.props.remark.id,
      username: this.props.remark.username,
      note: this.props.remark.note,
      latitude: this.props.remark.latitude,
      longitude: this.props.remark.longitude,
      dateCreated: dateStr
    }
  }

  render() {
    return (
      <div className={"remark " + (this.state.isCurrentUser ? 'currentuser' : 'otheruser')}>
        <Typography variant="overline" gutterBottom>
          {this.state.dateCreated}
        </Typography>
        <Typography variant="h6" gutterBottom>
          {this.state.username}
        </Typography>
        <Typography variant="subtitle1" gutterBottom>
          {this.state.note}
        </Typography>
        <Typography variant="body1" gutterBottom>
          Latitude: {this.state.latitude}<br/>
          Longitude: {this.state.longitude}
        </Typography>
        <Divider />
      </div>
    );
  }
}
