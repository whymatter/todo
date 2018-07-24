import {Injectable} from '@angular/core';

@Injectable()
export class GrantService {

  constructor() {
  }

  getAccessToken(): string {
    return 'Bearer eyJhbGciOiJIUzI1NiJ9.eyJ1c2VySWQiOiIwIiwic3ViIjoiaWppIiwiaWF0IjoxNTMwODM0MjYzLCJpc3MiOiJ0b2RvLmNvbSIsImF1ZCI6InRvZG8uY29tIiwiZXhwIjoxNTYyMzcwMjYzfQ.3unlIV29s4GqNJkQkQLVk8lZcLyp1i1n4GH-MyAVMfw';
  }

}
