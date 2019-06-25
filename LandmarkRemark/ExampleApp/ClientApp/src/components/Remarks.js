import React, {Component} from 'react';
import {Remark} from "./Remark";
import TextField from "@material-ui/core/TextField";
import FormControl from "@material-ui/core/FormControl";


export class Remarks extends Component {
  static displayName = Remarks.name;
  
  constructor(props) {
    super(props);
    
    this.state = {
      currentFilter: ''
    }
  }

  handleFilterChange = currentFilter => event => {
    this.setState({
      [currentFilter]: event.target.value,
    });
  };

  render() {
    // filter the remarks by the filter text
    let filtered = this.props.remarks.filter(remark => {
      let trimLoweredFilter = this.state.currentFilter.toLowerCase().trim();
      let trimLoweredNote = remark.note.toLowerCase().trim();
      let trimLoweredUser = remark.username.toLowerCase().trim();
      
      return trimLoweredNote.includes(trimLoweredFilter) ||
        trimLoweredUser.includes(trimLoweredFilter)
    });
    
    // the child components to render
    const renderedRemarks = filtered.map(remark =>
        <Remark remark={remark} key={remark.id} />
    );

    return (
      <div>
        <div className="page-container">
          <FormControl fullWidth style={{backgroundColor: `#f7f7f7`}}>
            <TextField
              id="filled-name"
              label="Search"
              value={this.state.currentFilter}
              onChange={this.handleFilterChange('currentFilter')}
            />
          </FormControl>
          
          <div className="remarks-list">
            {renderedRemarks}
          </div>
        </div>
      </div>
    );
  }
}
