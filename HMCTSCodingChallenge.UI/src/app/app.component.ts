import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { Task } from '../models/task.model';
import { TaskService } from '../services/task.service';
import { CommonModule } from '@angular/common';
import { FormGroup, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';

interface Status {
  value: string;
  viewValue: string;
}

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule,
    MatGridListModule,
    MatInputModule,
    MatFormFieldModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSelectModule,
    MatButtonModule
  ],
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './app.component.html',
})
export class AppComponent implements OnInit {
  tasks: Task[] = [];

  taskForm = new FormGroup({
    title: new FormControl('', [Validators.required]),
    description: new FormControl(''),
    status: new FormControl('Pending', [Validators.required]),
    dueDate: new FormControl<Date | null>(null, [Validators.required])
  });

  statuses: Status[] = [
    { value: 'Pending', viewValue: 'Pending' },
    { value: 'In Progress', viewValue: 'In Progress' },
    { value: 'Completed', viewValue: 'Completed' }
  ];

  constructor(private taskService: TaskService) {}

  ngOnInit() {
    this.loadTasks();
  }

  loadTasks() {
    this.taskService.getAll().subscribe(tasks => {
      this.tasks = tasks;
    });
  }

  createTask() {
    if (this.taskForm.invalid) return;

    const formValue = this.taskForm.value;

    const newTask: Task = {
      title: formValue.title ?? '',
      description: formValue.description ?? '',
      status: formValue.status ?? '',
      dueDate: formValue.dueDate instanceof Date
        ? formValue.dueDate.toISOString()
        : formValue.dueDate ?? ''
    };

    this.taskService.create(newTask).subscribe(() => {
      this.taskForm.reset({ status: 'Pending' });
      this.loadTasks();
    });
  }

  deleteTask(id: number) {
    this.taskService.delete(id).subscribe(() => this.loadTasks());
  }
}
